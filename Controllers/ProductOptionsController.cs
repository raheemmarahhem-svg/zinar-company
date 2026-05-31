using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZinarCompany.Models;

namespace ZinarCompany.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductOptionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductOptionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // List specifications for one product
        public async Task<IActionResult> Index(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null) return NotFound();

            ViewBag.ProductId = productId;
            ViewBag.ProductName = product.Name;

            var specs = await _context.ProductOptions
                .Where(x => x.ProductId == productId)
                .OrderBy(x => x.Name)
                .ToListAsync();

            return View(specs);
        }

        // GET: Create specification
        public IActionResult Create(int productId)
        {
            return View(new ProductOption { ProductId = productId });
        }

        // POST: Create specification
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductOption spec)
        {
            ModelState.Remove("Product");
            if (!ModelState.IsValid)
                return View(spec);

            _context.ProductOptions.Add(spec);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index" ,  new { productId = spec.ProductId });
        }
        // GET: ProductOptions/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var spec = await _context.ProductOptions.FindAsync(id);
            if (spec == null) return NotFound();

            return View(spec);
        }

        // POST: ProductOptions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductOption spec)
        {
            if (id != spec.Id) return NotFound();

            if (!ModelState.IsValid)
                return View(spec);

            _context.Update(spec);
            await _context.SaveChangesAsync();

            // go back to list of specs of same product
            return RedirectToAction(nameof(Index), new { productId = spec.ProductId });
        }
        // GET: Delete specification
        public async Task<IActionResult> Delete(int id)
        {
            var spec = await _context.ProductOptions
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (spec == null) return NotFound();
            return View(spec);
        }

        // POST: Delete specification
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var spec = await _context.ProductOptions.FindAsync(id);
            if (spec == null) return NotFound();

            int productId = spec.ProductId;

            _context.ProductOptions.Remove(spec);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { productId });
        }
    }
}