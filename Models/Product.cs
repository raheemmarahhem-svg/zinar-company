using System.ComponentModel.DataAnnotations;

namespace ZinarCompany.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string MaterialType { get; set; } = string.Empty; // Wood or Aluminum

        public decimal? Price { get; set; }   // make nullable if you removed price from the form

        [StringLength(500)]
        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public string? ShortDec { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public List<ProductOption> ProductOptions { get; set; } = new();
    }
}