﻿using SM.Data.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SM.Data.Models.Models
{
    [Table("Smartphone")]
    public class Smartphone : IBaseModel
    {
        public Smartphone()
        {
            this.OrderDetail = new HashSet<OrderDetail>();
            this.CartDetail = new HashSet<CartDetail>();
        }

        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? ShortDescription { get; set; }
        [Required]
        public string? LongDescription { get; set; }
        [Required]
        public decimal Price { get; set; }

        public string? Image { get; set; }

        [Required]
        public Guid BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public ICollection<OrderDetail> OrderDetail { get; set; }
        public ICollection<CartDetail> CartDetail { get; set; }
        public Stock Stock { get; set; }

        [NotMapped]
        public string BrandName { get; set; }
        [NotMapped]
        public int Quantity { get; set; }


    }
}
