using BookStore.Entites;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(AuthenticatedUser authenticatedUser)
        {
            authenticatedUser.UserName = authenticatedUser.Email;
            var result = _authService.AddUser(authenticatedUser);

            if(result == 1)
            {
                return RedirectToAction(nameof(Login));
            }

            return View();
        }
    }
}
