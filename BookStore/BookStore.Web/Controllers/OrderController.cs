using BookStore.Entites;
using BookStore.Services;
using BookStore.Web.Filter;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class OrderController : Controller
    {
        private IBookStoreService _bookStoreService;

        public OrderController(IBookStoreService bookStoreService)
        {
            _bookStoreService = bookStoreService;
        }

        [CheckSession("userId")]
        public IActionResult Index()
        {
            List<OrderDetail> orders;
            if (HttpContext.Session.GetString("role") == "Admin")
            {
                orders = _bookStoreService.GetAllOrders();
                return View(orders);
            }
            var userId = HttpContext.Session.GetInt32("userId");
            orders = _bookStoreService.GetAllOrders(userId);
            return View(orders);
        }
    }
}
