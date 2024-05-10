using SM.Data.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Data.Models.DTOs
{
    public class DisplayPhone
    {
        public IEnumerable<Smartphone> Smartphones { get; set; }

        public IEnumerable<Brand> Brands { get; set; }

        public string STerm { get; set; } = "";

        public Guid BrandId { get; set; } = Guid.Empty;
    }
}
