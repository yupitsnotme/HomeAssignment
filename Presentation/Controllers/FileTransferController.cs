using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
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
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(FileTransferViewModel model, IFormFile logoFile)
        {
            //logger.Log(LogLevel.Information, $"{User.Identity.Name} is uploading a file called {logoFile.FileName}");

            try
            {
                if (ModelState.IsValid)
                {
                    if (logoFile != null)
                    {
                        //1. to generate a new unique filename
                        //5389205C-813B-4AFA-A453-B912C30BF933.jpg
                        string newFilename = Guid.NewGuid() + Path.GetExtension(logoFile.FileName);
                        logger.Log(LogLevel.Information, $"New filename {newFilename} was generated for the file being uploaded by user {User.Identity.Name}");
                        //2. find what the absolute path to the folder Files is
                        //C:\Users\attar\Source\Repos\SWD62BEP2021v2\Presentation\Files\5389205C-813B-4AFA-A453-B912C30BF933.jpg

                        //hostEnv.ContentRootPath : C:\Users\attar\Source\Repos\SWD62BEP2021v2\Presentation
                        //hostEnv.WebRootPath:  C:\Users\attar\Source\Repos\SWD62BEP2021v2\Presentation\wwwroot

                        string absolutePath = hostEnv.WebRootPath + "\\Files";
                        logger.Log(LogLevel.Information, $"{User.Identity.Name} is about to start saving file at {absolutePath}");

                        string absolutePathWithFilename = absolutePath + "\\" + newFilename;
                        model.FileName = "\\Files\\" + newFilename;
                        //3. do the transfer/saving of the actual physical file

                        using (FileStream fs = new FileStream(absolutePathWithFilename, FileMode.CreateNew, FileAccess.Write))
                        {
                            logoFile.CopyTo(fs);
                            fs.Close();
                        }
                        logger.Log(LogLevel.Information, $"{newFilename} has been saved successfully at {absolutePath}");
                    }

                    fileTransfersService.AddFile(model);
                    ViewBag.Message = "File added successfully";
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = "File wasn't added successfully";
            }

            return View();
        }
    }
}
