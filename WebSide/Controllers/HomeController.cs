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
using WebSide.Services;

namespace WebSide.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IWebHostEnvironment _appEnvironment;
        UsersModel _user;
        BookModel _book;
        SessionsService _sessions;
        
        private void UserSync()
        {
            if (HttpContext.Session.Keys.Contains("UserID"))
            {
                _user = _sessions.TryGetUserById(Guid.Parse(HttpContext.Session.GetString("UserID")));
            }
            else
            {
                Guid guid = Guid.NewGuid();
                HttpContext.Session.SetString("UserID", guid.ToString());
                _sessions.AddUser(new UsersModel { Id = guid });
                _user = _sessions.TryGetUserById(guid);
            }
        }

        public HomeController(IWebHostEnvironment hostEnvironment, ILogger<HomeController> logger, SessionsService sessions)
        {
            _appEnvironment = hostEnvironment;
            _logger = logger;
            _sessions = sessions;
        }

        public IActionResult Index()
        {
            UserSync();

            if (_user.Book == null)
                return View();
            else
                return View(_user.Book);

            //return new ContentResult {
            //    Content = $"{HttpContext.Session.GetString("UserID")}"
            //};
            //
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
            UserSync();
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
                _user.Book = _book;
            }
            
            return RedirectToAction("Index");
        }

    }
}
