using BookStore.Entites;
using BookStore.Services;
using BookStore.Web.Filter;
using BookStore.Web.ViewModels;
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

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (_authService.CheckUserExists(model.UserName, model.Password))
            {

                var user = _authService.CheckUser(model.UserName, model.Password);

                if(user != null)
                {
                    var role = _authService.GetRole(user.RoleId);

                    HttpContext.Session.SetString("userName",user.UserName);
                    HttpContext.Session.SetString("role",role.Name);
                    HttpContext.Session.SetString("userId",user.Id.ToString());

                    return RedirectToAction("Index", "Home");
                }
            }


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

            if(result > 0)
            {
                return RedirectToAction(nameof(Login));
            }

            return View();
        }
    }
}
