using AuthToken.Business.Services.Interfaces;
using AuthToken.ViewModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthToken.Controllers
{
    [Route("api/Auth")]
    public class AuthController : Controller
    {
        private readonly IAccountService _accountService;

        public AuthController(IBooksService booksService, IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("LogIn")]
        [AllowAnonymous]
        public IActionResult LogIn(SignInViewModel model)
        {
            var result = _accountService.SignIn("berdyshev1997@gmail.com", "Qwe123!!");

            return Ok(result);
        }
    }
}