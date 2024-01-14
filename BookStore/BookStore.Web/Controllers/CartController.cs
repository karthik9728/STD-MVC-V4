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

        [CheckSession("userId")]
        public IActionResult ChangeAddress()
        {
            UserViewModel vm = new UserViewModel();
            var userDetail = _authService.GetUserByUserId((int)HttpContext.Session.GetInt32("userId"));
            vm.Id = userDetail.Id;
            vm.Address = userDetail.Address;
            vm.ContactNumber = userDetail.ContactNumber;
            return View(vm);
        }

        [HttpPost]
        public IActionResult ChangeAddress(UserViewModel vm)
        {
            int result = _authService.UpdateUserDetails(vm.Id, vm.Address, vm.ContactNumber);
            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }

        [CheckSession("userId")]
        public IActionResult AddToOrder()
        {
            OrderDetail orderDetail = new OrderDetail();
            var userCart = _bookService.GetCartDetailsByUserId((int)HttpContext.Session.GetInt32("userId"));
            var result = _bookService.AddCartToOrder(userCart);
            if (result == true)
            {
                int userId = (int)HttpContext.Session.GetInt32("userId");
                TempData["success"] = "Thanks For Your Order";
                _bookService.DeleteAllCartItemsByUserId(userId);
                HttpContext.Session.SetInt32("sessionCart", _bookService.GetCartDetailsByUserId(userId).Count());
                return View("success");
            }
            return View(orderDetail);
        }
    }
}
