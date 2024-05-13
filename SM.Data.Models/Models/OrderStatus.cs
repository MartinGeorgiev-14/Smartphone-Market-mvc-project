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
    [Table("OrderStatus")]
    public class OrderStatus : IBaseModel
    {
        public Guid Id { get; set; }

        [Required, MaxLength(20)]
        public string? StatusName { get; set; }

        [NotMapped]
        public object Smartphone { get; set; }
    }
}
