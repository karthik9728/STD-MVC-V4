using BookStore.Services;
using BookStore.Web.Filter;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class CartController : Controller
    {

        private IBookStoreService _bookService;
        private IAuthService _authService;

        public CartController(IBookStoreService bookService, IAuthService authService)
        {
            _bookService = bookService;
            _authService = authService;
        }


        [CheckSession("userId")]
        public IActionResult Index()
        {
            var carts = _bookService.GetCartDetailsByUserId((int)HttpContext.Session.GetInt32("userId"));
            return View(carts);
        }
    }
}
