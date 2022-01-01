using Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class FileTransferController : Controller
    {
        private IFileTransfersService fileTransfersService;
        private IWebHostEnvironment hostEnv;
        private ILogger<FileTransferController> logger;

        public FileTransferController(ILogger<FileTransferController> logger, IFileTransfersService fileTransfersService, IWebHostEnvironment hostEnv)
        {
            this.fileTransfersService = fileTransfersService;
            this.hostEnv = hostEnv;
            this.logger = logger;
        }
        public IActionResult Index()
        {   
            return View();
        }
    }
}
