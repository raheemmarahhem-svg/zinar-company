namespace ZinarCompany.ViewModels
{
    public class QuoteLineVM
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public int Quantity { get; set; }


        // for cart UI (image + small text)
        public string? ImageUrl { get; set; }
        public string ShortDesc { get; set; }
        
    }
}