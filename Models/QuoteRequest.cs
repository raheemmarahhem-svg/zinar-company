using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZinarCompany.Models
{
    public class QuoteRequest
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = "";   // ✅ never null

        public string? Phone { get; set; }           // ✅ nullable
        public string? City { get; set; }            // ✅ nullable
        public string? Notes { get; set; }           // ✅ nullable

        public ICollection<QuoteItem> QuoteItems { get; set; } = new List<QuoteItem>();
        public List<QuoteRequestItem> Items { get; set; } = new();
    }
}