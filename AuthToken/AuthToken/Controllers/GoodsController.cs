using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthToken.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthToken.Controllers
{
    [Route("api/Goods")]
    public class GoodsController : Controller
    {
        private readonly IBooksRepository _booksRepository;

        public GoodsController(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public IActionResult Get()
        {


            return Ok();
        }
    }
}