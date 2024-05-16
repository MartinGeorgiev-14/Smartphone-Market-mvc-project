using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Data.Models.DTOs
{
    public class BrandDTO
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(40)]

        public string BrandName { get; set; }   
    }
}
