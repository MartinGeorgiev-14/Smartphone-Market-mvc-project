using SM.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SM.Data.Models.Models
{
    [Table("CartDetail")]
    public class CartDetail : IBaseModel
    {
        public Guid Id { get; set; }
        [Required]
        public Guid ShoppingCartId { get; set; }
        [Required]
        public Guid PShoeId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        public PShoe PShoe { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
