using AuthToken.Business.Services.Interfaces;
using AuthToken.ViewModels.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public IActionResult LogIn(LogInAuthViewModel model)
        {
            var result = _accountService.LogIn("berdyshev1997@gmail.com", "Qwe123!!");

            return Ok(result);
        }


        [HttpPost]
        [Route("SignUp")]
        [AllowAnonymous]
        public IActionResult SignUp(SignUpAuthViewModel model)
        {
            SignUpAuthViewModel view = new SignUpAuthViewModel();
            view.Birthday = DateTime.Now;
            view.Email = "daryaaay@gmail.com";
            view.FirstName = "Daria";
            view.LastName = "Yanieva";
            view.Password = "Qwe123!!";

            _accountService.SignUp(view);

            return Ok();
        }


        [HttpGet]
        [Route("ConfirmEmail")]
        [AllowAnonymous]
        public IActionResult ConfirmEmail(ConfirmEmailAuthViewModel model)
        {
            var result = _accountService.ConfirmEmail(model);

            return Ok(result);
        }
    }
}