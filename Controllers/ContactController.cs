using Microsoft.AspNetCore.Mvc;

namespace ZinarCompany.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}