using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebSide.Models;

namespace WebSide.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IWebHostEnvironment _appEnvironment;
        BookModel _book;
        
        public HomeController(IWebHostEnvironment hostEnvironment, ILogger<HomeController> logger)
        {
            _appEnvironment = hostEnvironment;
            _logger = logger;
        }

        public IActionResult Index()
        {

            return new ContentResult {
                Content = $"{HttpContext.Session.Keys}"
            };
            //return View(_context.Files.ToList());
        }   


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [DisableRequestSizeLimit]
        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            _book = new BookModel();

            if (uploadedFile != null)
            {
                string _savePath = _appEnvironment.WebRootPath + $"/files/{DateTime.Now:yyyy}-{DateTime.Now:MM}-{DateTime.Now:dd}/";

                _book.OriginBookPath = _savePath + _book.Id + ".pdf";

                Directory.CreateDirectory(_savePath);

                using (var fileStream = new FileStream(_book.OriginBookPath, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                _book.FileName = uploadedFile.FileName;
            }

            return RedirectToAction("Index");
        }
    }
}
