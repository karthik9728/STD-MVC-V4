using BookStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private IBookStoreService _bookService;

        public CartViewComponent(IBookStoreService bookService)
        {
            _bookService = bookService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (HttpContext.Session.GetInt32("userId") != null)
            {
                int userId = Convert.ToInt32(HttpContext.Session.GetInt32("userId"));
                HttpContext.Session.SetInt32("sessionCart", _bookService.GetCartDetailsByUserId(userId).Count());
                return View(HttpContext.Session.GetInt32("sessionCart"));

            }
            else
            {
                HttpContext.Session.Clear();
                return View(0);

            }

        }
    }
}
