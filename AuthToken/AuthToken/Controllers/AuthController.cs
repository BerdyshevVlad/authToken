using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthToken.Controllers
{
    [Route("api/Auth")]
    public class AuthController : Controller
    {
        [HttpGet]
        [Route("LogIn")]
        public IActionResult Get()
        {
            return Ok(new string[] { "Perfecto!" });
        }
    }
}