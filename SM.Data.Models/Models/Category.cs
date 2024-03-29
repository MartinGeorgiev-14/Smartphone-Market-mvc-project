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
    [Table("Category")]
    public class Category : IBaseModel
    {
        public Category()
        {
            this.PShoes = new HashSet<PShoe>();
        }

        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }
        public ICollection<PShoe> PShoes { get; set; }
    }
}
