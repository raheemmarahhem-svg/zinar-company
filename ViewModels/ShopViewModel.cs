using System.Collections.Generic;
using ZinarCompany.Models;

namespace ZinarCompany.ViewModels
{
    public class ShopVM
    {
        public List<Category> Categories { get; set; } = new();
        public List<Product> Products { get; set; } = new();
        public int? CategoryId { get; set; }
    }
}