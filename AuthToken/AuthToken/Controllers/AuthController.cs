using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthToken.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        [Route("LogIn")]
        public IActionResult LogIn()
        {
            var result = _accountService.SignIn("berdyshev1997@gmail.com", "Qwe123!!");

            return Ok(result); 
        }
    }
}