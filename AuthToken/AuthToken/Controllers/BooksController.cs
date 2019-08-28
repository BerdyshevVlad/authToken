using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthToken.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthToken.Controllers
{
    [Route("api/Books")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BooksController : Controller
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }


        [HttpGet]
        [Route("getBooks")]
        [AllowAnonymous]
        public IActionResult Get()
        {
            var view = _booksService.GetList();

            return Ok(view);
        }


        [HttpGet]
        [Route("get/bookId/{bookId}")]
        public IActionResult GetById(int bookId)
        {
            var view = _booksService.GetById(bookId);

            return Ok(view);
        }
    }
}