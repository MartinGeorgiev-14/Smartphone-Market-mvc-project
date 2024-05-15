using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Data.Models.DTOs
{
    public class StockDisplayModel
    {
        public Guid Id { get; set; }

        public Guid SmartphoneId { get; set; }

        public int Quantity { get; set; }

        public string? SmartphoneName { get; set; }

        public string? BrandName { get; set; }  
    }
}
