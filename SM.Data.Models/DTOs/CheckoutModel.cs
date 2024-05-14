using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Data.Models.DTOs
{
    public class CheckoutModel
    {
        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string? Email { get; set; }

        [Required]
        public string? MobileNumber { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Address { get; set; }

        [Required]
        public string? PaymentMethod { get; set; }
    }
}
