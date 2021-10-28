using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/Error/{code:int}")]
        public IActionResult ErrorHandler(int code)
        {
            if (code == 404)
            {
                return View("Error404");
            }

            return View("ErrorDefault");
        }
    }
}
