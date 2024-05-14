using SM.Data.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Data.Models.DTOs
{
    public class OrderDetailModel
    {
        public Guid Id { get; set; }
        public IEnumerable<OrderDetail> OrderDetail { get; set; }
    }
}
