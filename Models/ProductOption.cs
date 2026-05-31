using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ZinarCompany.Models
{
    public class ProductOption
    {
        public int Id { get; set; }

        public string GroupName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public int ProductId { get; set; }

        [ValidateNever]
        public Product? Product { get; set; }
    }
}