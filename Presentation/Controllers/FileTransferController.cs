using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class FileTransferController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create() {

            return View();
        }
    }
}
