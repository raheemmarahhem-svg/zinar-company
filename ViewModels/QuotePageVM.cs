using System.Collections.Generic;

namespace ZinarCompany.ViewModels
{
    public class QuotePageVM
    {
        public List<QuoteLineVM> Lines { get; set; } = new();

        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? Notes { get; set; }
    }
}