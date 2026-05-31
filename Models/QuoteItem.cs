using System.ComponentModel.DataAnnotations;

namespace ZinarCompany.Models
{
    public class QuoteItem
    {
        public int Id { get; set; }

        public int QuoteRequestId { get; set; }
        public QuoteRequest QuoteRequest { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Range(1, 999)]
        public int Quantity { get; set; } = 1;

        // Optional (only if you still want notes later)
        // public string? Notes { get; set; }
    }
}