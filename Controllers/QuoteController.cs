using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZinarCompany.Models;
using ZinarCompany.ViewModels;

namespace ZinarCompany.Controllers
{
    public class QuoteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuoteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Always use ONE "Guest" QuoteRequest as the cart owner
        private QuoteRequest GetOrCreateGuestCart()
        {
            // If QuoteRequests table doesn't exist, you'll get "Invalid object name QuoteRequests"
            // (fix = run migrations, see steps below)

            var cart = _context.QuoteRequests
                .OrderByDescending(q => q.Id)
                .FirstOrDefault(q => q.FullName == "Guest");

            if (cart == null)
            {
                cart = new QuoteRequest
                {
                    FullName = "Guest",
                    Phone = "",
                    City = "",
                    Notes = ""
                };

                _context.QuoteRequests.Add(cart);
                _context.SaveChanges();
            }

            return cart;
        }

        // ✅ Quote cart page
        public IActionResult Index()
        {
            var cart = GetOrCreateGuestCart();

            var lines = _context.QuoteItems
                .Include(q => q.Product)
                .Where(q => q.QuoteRequestId == cart.Id)
                .OrderByDescending(q => q.Id)
                .Select(q => new QuoteLineVM
                {
                    ProductId = q.ProductId,
                    ProductName = q.Product != null ? q.Product.Name : "",
                    Quantity = q.Quantity,
                    ImageUrl = q.Product != null ? q.Product.ImageUrl : null,
                    ShortDesc = q.Product != null ? q.Product.Description : null
                })
                .ToList();

            var vm = new QuotePageVM { Lines = lines };
            return View(vm);
        }

        // Add = same as Increase
        public IActionResult Add(int productId)
        {
            return RedirectToAction(nameof(Increase), new { productId });
        }

        public IActionResult Increase(int productId)
        {
            var cart = GetOrCreateGuestCart();

            var item = _context.QuoteItems
                .FirstOrDefault(x => x.ProductId == productId && x.QuoteRequestId == cart.Id);

            if (item == null)
            {
                _context.QuoteItems.Add(new QuoteItem
                {
                    QuoteRequestId = cart.Id,   // ✅ IMPORTANT
                    ProductId = productId,
                    Quantity = 1
                });
            }
            else
            {
                item.Quantity += 1;
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Decrease(int productId)
        {
            var cart = GetOrCreateGuestCart();

            var item = _context.QuoteItems
                .FirstOrDefault(x => x.QuoteRequestId == cart.Id && x.ProductId == productId);

            if (item == null) return RedirectToAction(nameof(Index));

            if (item.Quantity > 1)
                item.Quantity -= 1;
            else
                _context.QuoteItems.Remove(item);

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int productId)
        {
            var cart = GetOrCreateGuestCart();

            var item = _context.QuoteItems
                .FirstOrDefault(x => x.QuoteRequestId == cart.Id && x.ProductId == productId);

            if (item != null)
            {
                _context.QuoteItems.Remove(item);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Clear()
        {
            var cart = GetOrCreateGuestCart();

            var items = _context.QuoteItems
                .Where(x => x.QuoteRequestId == cart.Id)
                .ToList();

            _context.QuoteItems.RemoveRange(items);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // ✅ SUBMIT FORM (creates real request + copies cart items into QuoteRequestItems)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(QuotePageVM vm)
        {
            if (string.IsNullOrWhiteSpace(vm.FullName) || string.IsNullOrWhiteSpace(vm.Phone))
            {
                TempData["QuoteError"] = "Please enter your name and phone number.";
                return RedirectToAction(nameof(Index));
            }

            var cart = GetOrCreateGuestCart();

            // Read cart items
            var cartItems = _context.QuoteItems
                .Where(x => x.QuoteRequestId == cart.Id)
                .ToList();

            if (cartItems.Count == 0)
            {
                TempData["QuoteError"] = "Your cart is empty.";
                return RedirectToAction(nameof(Index));
            }

            // 1) Create the REAL customer request
            var request = new QuoteRequest
            {
                FullName = vm.FullName!,
                Phone = vm.Phone!,
               
                Notes = vm.Notes
            };

            _context.QuoteRequests.Add(request);
            _context.SaveChanges(); // request.Id exists

            // 2) Copy cart items into QuoteRequestItems
            foreach (var item in cartItems)
            {
                _context.QuoteRequestItems.Add(new QuoteRequestItem
                {
                    QuoteRequestId = request.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                });
            }

            _context.SaveChanges();

            // 3) Clear cart items (guest cart)
            _context.QuoteItems.RemoveRange(cartItems);
            _context.SaveChanges();

            TempData["QuoteSuccess"] = "Your quote request has been sent!";
            return RedirectToAction(nameof(Index));
        }
    }
}