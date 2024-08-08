using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.Models.DataModel
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public DateTime? DateCreated { get; set; }
    }
}
