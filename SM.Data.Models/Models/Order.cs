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
    [Table("Order")]
    public class Order : IBaseModel
    {
        public Order()
        {
            this.OrderDetail = new HashSet<OrderDetail>();
        }

        public Guid Id { get; set; }
        [Required]
        public string? UserId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        [Required]
        public Guid OrderStatusId { get; set; }
        public bool IsDeleted { get; set; } = false;

        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? MobileNumber { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Address { get; set; }

        [Required]
        [MaxLength(30)]
        public string? PaymentMethod { get; set; }

        public bool isPaid { get; set; }


        public OrderStatus OrderStatus { get; set; }
        public ICollection<OrderDetail> OrderDetail { get; set; }

    }
}
