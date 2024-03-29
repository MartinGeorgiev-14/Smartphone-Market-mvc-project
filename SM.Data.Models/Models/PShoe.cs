using SM.Data.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SM.Data.Models.Models
{
    [Table("PShoe")]
    public class PShoe : IBaseModel
    {
        public PShoe()
        {
            this.OrderDetail = new HashSet<OrderDetail>();
            this.CartDetail = new HashSet<CartDetail>();
        }

        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? ShortDescription { get; set; }
        [Required]
        public string? LongDescription { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string? ImageUrl { get; set; }
        [Required]
        public string? ImageTumbnailImg { get; set; }
        [Required]
        public int InStock { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public Guid BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public ICollection<OrderDetail> OrderDetail { get; set; }
        public ICollection<CartDetail> CartDetail { get; set; }


    }
}
