﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SM.Data.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        { }

        public DbSet<Smartphone> Smartphones { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }


        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        //    base.OnModelCreating(builder);

        //    // One-to-many relationship between Boot and Brand
        //    builder.Entity<PShoe>()
        //        .HasOne(boot => boot.Brand)
        //        .WithMany(brand => brand.PShoes)
        //        .HasForeignKey(boot => boot.BrandId);

        //    // One-to-many relationship between Boot and Category
        //    builder.Entity<PShoe>()
        //        .HasOne(boot => boot.Category)
        //        .WithMany(category => category.PShoes)
        //        .HasForeignKey(boot => boot.CategoryId);


        //}
    }
}
