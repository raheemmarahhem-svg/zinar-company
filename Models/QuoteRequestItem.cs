using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZinarCompany.Models
{
    public class QuoteRequestItem
    {
        public int Id { get; set; }

        [Required]
        public int QuoteRequestId { get; set; }
        public QuoteRequest? QuoteRequest { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [Range(1, 9999)]
        public int Quantity { get; set; }
    }
}