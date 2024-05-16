using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Data.Models.DTOs
{
    public class SmartphoneDTO
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string? SmartphoneName { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string? Image { get; set; }

        [Required]
        public string? ShortDescription { get; set; }

        [Required]
        public string? LongDescription { get; set; }

        [Required]
        public Guid BrandId { get; set; }

        public IFormFile? ImageFile { get; set; }

        public IEnumerable<SelectListItem>? BrandList { get; set; }
    }
}
