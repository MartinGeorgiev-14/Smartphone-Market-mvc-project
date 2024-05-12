using SM.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Data.Models.Models
{
    public class Stock : IBaseModel
    {
        public Guid Id { get; set; }
        public Guid SmartphoneId { get; set; }
        public int Quantity { get; set; }

        public Smartphone? Smartphone { get; set; }
    }
}
