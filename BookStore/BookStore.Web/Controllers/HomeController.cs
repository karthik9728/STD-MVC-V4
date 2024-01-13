using BookStore.Services;
using BookStore.Web.Filter;
using BookStore.Web.Models;
using BookStore.Web.ViewModels;
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

        //[CheckSession("userName")]
        public IActionResult Index()
        {
     
            var books = _bookService.GetListOfBooks();

            List<BookViewModel> vm = new List<BookViewModel>();   

            foreach (var book in books)
            {
                vm.Add(new BookViewModel
                {
                    BookId = book.Id, 
                    PictureUri = book.BookImage,
                    Price = book.Price,
                    Name = book.Name,
                });
            }

            return View(vm);
        }

        public IActionResult Details(int id)
        {
            BookWithCategory vm = new BookWithCategory();

            var bookDetails = _bookService.GetBookById(id);

            if(bookDetails.BookAuthorId != 0)
            {
                var bookAuthor = _bookService.GetBookAuthorById(bookDetails.BookAuthorId);

                vm.Author = bookAuthor;
            }

            if(bookDetails.BookStoreId != 0)
            {
                var bookStore = _bookService.GetStoryById(bookDetails.BookStoreId);

                vm.BookStore = bookStore;
            }

            vm.Categories = _bookService.GetCategoriesByBookId(id);
            vm.BookId = bookDetails.Id;
            vm.Name = bookDetails.Name;
            vm.ISBNNumber = bookDetails.ISBNNumber;
            vm.Price = bookDetails.Price;
            vm.PictureUri = bookDetails.BookImage;
            vm.Description = bookDetails.Description;

            return View(vm);
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
