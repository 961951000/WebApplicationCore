using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationCore.Controllers
{
    public class NetworkDiskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}