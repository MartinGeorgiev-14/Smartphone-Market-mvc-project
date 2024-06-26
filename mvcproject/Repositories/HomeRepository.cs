﻿using mvcproject.Repositories.Interfaces;
using SM.Data.Models.Models;
using SM.Data;
using Microsoft.EntityFrameworkCore;

namespace mvcproject.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _db;

        public HomeRepository(ApplicationDbContext db)
        {
            this._db = db;
        }

        public async Task<IEnumerable<Brand>> Brands()
        {
            return await _db.Brands.ToListAsync();
        }

        public async Task<IEnumerable<Smartphone>> GetPhones(Guid brandId, string sTrem = "")
        {
            sTrem = sTrem.ToLower();
            //Joining Smartphones table and Brands table
            IEnumerable<Smartphone> phones = await (from phone in _db.Smartphones
                                                    join brand in _db.Brands
                                                    on phone.BrandId equals brand.Id
                                                    join stock in _db.Stocks
                                                    on phone.Id equals stock.SmartphoneId
                                                    into phone_stocks
                                                    from phoneWithStocks in phone_stocks.DefaultIfEmpty()
                                                    //If there is no search string it returns all records                  //All records starting with search string will be filtered
                                                    /*------>*/
                                                    where string.IsNullOrWhiteSpace(sTrem) || (phone != null && phone.Name.ToLower().StartsWith(sTrem))
                                                    select new Smartphone
                                                    {
                                                        Id = phone.Id,
                                                        Image = phone.Image,
                                                        Name = phone.Name,
                                                        BrandId = phone.BrandId,
                                                        Brand = phone.Brand,
                                                        Price = phone.Price,
                                                        ShortDescription = phone.ShortDescription,
                                                        Quantity = phoneWithStocks == null ? 0 : phoneWithStocks.Quantity
                                                    }
                          ).ToListAsync();

            //Checking if getting brandID then will be filtered based on brandID
            if (brandId != Guid.Empty)
            {
                phones = phones.Where(a => a.BrandId == brandId).ToList();
            }
            return phones;
        }

    }
}
