using SM.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Data.Models.Models
{
    [Table("Brand")]
    public class Brand : IBaseModel
    {
        public Brand()
        {
            this.Smartphones = new HashSet<Smartphone>();
        }

        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }
        public ICollection<Smartphone> Smartphones { get; set; }
    }
}
