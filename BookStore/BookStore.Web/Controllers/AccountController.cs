using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
