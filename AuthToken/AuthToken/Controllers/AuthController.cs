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
        private readonly IBooksService _booksService;

        public AuthController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        [Route("LogIn")]
        public IActionResult Get()
        {
            _booksService.AddBook();

            return Ok(new string[] { "Perfecto!" });
        }
    }
}