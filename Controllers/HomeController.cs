using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RandomPasscode.Models;
using Microsoft.AspNetCore.Http;

namespace RandomPasscode.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        private string GeneratePasscode(int size)
        {
            Random random = new Random();
            string PassCodeChars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
            string result = "";
            for (int i = 0; i < size; i++)
            {
                result += PassCodeChars[random.Next(PassCodeChars.Length)];
            }
            return result;
        }

        [HttpGet ("")]
        public IActionResult Index()
        {

            if (HttpContext.Session.GetInt32("count") == null)
            {
                HttpContext.Session.SetInt32("count", 0);
            }
            if (HttpContext.Session.GetString("passcode") == null)
            {
                HttpContext.Session.SetString("passcode", "Click Generate to get a Random Passcode!");
            }
            ViewBag.Code = HttpContext.Session.GetString("passcode");
            ViewBag.Count = HttpContext.Session.GetInt32("count");
            return View();
        }
        

        [HttpGet ("click")]
        public IActionResult GenNew()
        {
            int? count = HttpContext.Session.GetInt32("count");
            count++;
            HttpContext.Session.SetInt32("count", (int)count);
            HttpContext.Session.SetString("passcode", GeneratePasscode(14));
            return RedirectToAction("Index");
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
    }
}
