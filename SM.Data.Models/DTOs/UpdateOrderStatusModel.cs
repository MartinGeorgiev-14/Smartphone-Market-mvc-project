using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SM.Data.Models.DTOs
{
    public class UpdateOrderStatusModel
    {
        public Guid Id { get; set; }

        [Required]

        public Guid OrderStatusId { get; set; }

        public IEnumerable<SelectListItem>? OrderStatusList { get; set; }
    }
}
