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

        public OrderStatus OrderStatus { get; set; }
        public ICollection<OrderDetail> OrderDetail { get; set; }

    }
}
