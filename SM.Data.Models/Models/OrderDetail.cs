using SM.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Data.Models.Models
{
    [Table("OrderDetail")]
    public class OrderDetail : IBaseModel
    {
        public Guid Id { get; set; }
        [Required]
        public Guid OrderId { get; set; }
        [Required]
        public Guid PShoeId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        public Order Order { get; set; }
        public Smartphone PShoe { get; set; }


    }
}
