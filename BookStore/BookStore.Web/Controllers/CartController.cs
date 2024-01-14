using BookStore.Entites;
using BookStore.Services;
using BookStore.Web.Filter;
using BookStore.Web.ViewModels;
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

        [HttpGet]
        [CheckSession("userId")]
        public IActionResult Edit(int id)
        {
            var cart = _bookService.GetCartById(id);

            return View(cart);
        }

        [HttpPost]
        public IActionResult Edit(Cart cart)
        {
            var totalAmount = (cart.Quantity) * (cart.Price);
            int result = _bookService.UpdateCart(cart.Id, totalAmount, cart.Quantity);
            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [CheckSession("userId")]
        public IActionResult Delete(int id)
        {
            var cart = _bookService.GetCartById(id);
            return View(cart);

        }

        [HttpPost]
        public IActionResult Delete(Cart cart)
        {
            int result = _bookService.DeleteCartById(cart.Id);
            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }

        [CheckSession("userId")]
        public IActionResult AddressDetail(Decimal GrandTotal)
        {
            BillingDetailViewModel vm = new BillingDetailViewModel();
            var userDetail = _authService.GetUserByUserId((int)HttpContext.Session.GetInt32("userId"));
            vm.Address = userDetail.Address;
            vm.GrandTotal = GrandTotal;
            return View(vm);

        }
    }
}
