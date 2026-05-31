using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZinarCompany.Models;

using ZinarCompany.ViewModels;

namespace ZinarCompany.Controllers
{
    public class ProductsController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ProductsController(
            ApplicationDbContext context,
        IWebHostEnvironment env
        )
        {
            _context = context;
            _env = env;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Shop(int? categoryId)
        {
            var categories = await _context.Categories
                .OrderBy(c => c.Name)
                .ToListAsync();

            var productsQuery = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            if (categoryId.HasValue)
                productsQuery = productsQuery.Where(p => p.CategoryId == categoryId.Value);

            var vm = new ShopVM
            {
                Categories = categories,
                Products = await productsQuery.ToListAsync(),
                CategoryId = categoryId
            };

            return View(vm);
        }
        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();

            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductOptions)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(
                _context.Categories,
                "Id",
                "Name"
            );

            return View();
        }

        // POST: Products/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Product product, IFormFile imageFile)
        {
            if (ModelState.IsValid)
              
                {
                    // 🔹 IMAGE SAVING LOGIC (GOES HERE)
                    if (imageFile != null && imageFile.Length > 0)
                    {
                    var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "products");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var uniqueFileName = Guid.NewGuid().ToString()
                        + Path.GetExtension(imageFile.FileName);

                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }

                    product.ImageUrl = "/uploads/products/" + uniqueFileName;
                }

                    // 🔹 SAVE PRODUCT AFTER IMAGE
                    _context.Add(product);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }

            ViewData["CategoryId"] = new SelectList(
                _context.Categories,
                "Id",
                "Name",
                product.CategoryId
            );

            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null) return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Product product, IFormFile imageFile)
        {
            if (id != product.Id)
                return NotFound();
            ModelState.Remove("imageFile");
            var dbProduct = await _context.Products.FindAsync(id);
            if (dbProduct == null) return NotFound();

            if (ModelState.IsValid)
            {
                // update normal fields
                dbProduct.Name = product.Name;
                dbProduct.MaterialType = product.MaterialType;
                dbProduct.Price = product.Price;
                dbProduct.Description = product.Description;
                dbProduct.CategoryId = product.CategoryId;

                // ✅ image update only if user selected a file
                if (imageFile != null && imageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "products");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    dbProduct.ImageUrl = "/uploads/products/" + uniqueFileName;
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null) return NotFound();

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }


// GET: Products/Aluminum
public IActionResult Aluminum()
{
    // Static page for Aluminum Type 1 (Doors & Windows)
    return View();
}

// GET: Products/Wood
public IActionResult Wood()
{
    // Static page for Wood decors
    return View();
}


// GET: Products/Furniture
public IActionResult Furniture()
{
    // Static page for Aluminum Furniture (Type 2)
    return View();
}


// GET: Products/Facade
public IActionResult Facade()
{
    // Static page for Facade Profiles (Type 3)
    return View();
}


// GET: Products/Sliding
public IActionResult Sliding()
{
    // Static page for Aluminum Sliding Profiles (Type 4)
    return View();
}
    }
}
