using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZinarCompany.Models;

namespace ZinarCompany.ViewComponents
{
    public class QuoteBadgeViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public QuoteBadgeViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var count = await _context.QuoteItems.SumAsync(q => (int?)q.Quantity) ?? 0;
            return View(count);
        }
    }
}