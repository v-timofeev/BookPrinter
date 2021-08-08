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
using System.Threading.Tasks;
using WebSide.Models;

namespace WebSide.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //ApplicationContext _context;
        IWebHostEnvironment _appEnvironment;

        public HomeController(IWebHostEnvironment hostEnvironment, ILogger<HomeController> logger)
        {
            //_context= context;
            _appEnvironment= hostEnvironment;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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

        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                string path ="/files/" + uploadedFile.FileName;

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                //BookModel book = new BookModel { Name = uploadedFile.FileName, Path = path };
                //_context.Files.Add(book);
                //_context.SaveChanges();

            }


            return RedirectToAction("Index");
        }
    }
}
