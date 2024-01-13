using BookStore.Services;
using BookStore.Web.Filter;
using BookStore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IBookStoreService _bookService { get; set; }

        public HomeController(ILogger<HomeController> logger, IBookStoreService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        [CheckSession("userName")]
        public IActionResult Index()
        {
            var book = _bookService.GetBookById(1);
            var books = _bookService.GetListOfBooks();

            return View(books);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
