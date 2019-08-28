using AuthToken.Business.Services.Interfaces;
using AuthToken.Extensions;
using AuthToken.ViewModels.Models.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AuthToken.Controllers
{
    [Route("api/Auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _accountService;

        public AuthController(IBooksService booksService, IAuthService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("LogIn")]
        [AllowAnonymous]
        public IActionResult LogIn([FromBody]LogInAuthViewModel model)
        {
            var result = _accountService.LogIn(model);

            return Ok(result);
        }


        [HttpPost]
        [Route("SignUp")]
        [AllowAnonymous]
        public IActionResult SignUp([FromBody]SignUpAuthViewModel model)
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


        [HttpPost]
        [Route("ConfirmEmail")]
        [AllowAnonymous]
        public IActionResult ConfirmEmail([FromBody] ConfirmEmailAuthViewModel model)
        {
            var result = _accountService.ConfirmEmail(model);

            return Ok(result);
        }


        [HttpPost]
        [Route("ForgotPassword")]
        [AllowAnonymous]
        public IActionResult ForgotPassword([FromBody]ForgotPasswordAuthViewModel model)
        {
            _accountService.ForgotPassword(model);
            return Ok();
        }


        [HttpPost]
        [Route("ResetPassword")]
        [AllowAnonymous]
        public IActionResult ResetPassword([FromBody]ResetPasswordAuthViewModel model)
        {
            _accountService.ResetPassword(model);
            return Ok();
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("changePassword")]
        public IActionResult ChangePassword([FromBody]ChangePasswordAuthViewModel model)
        {
            var email = User.Identity.GetUserEmail();

            _accountService.ChangePassword(email,model);
            return Ok();
        }
    }
}