using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Core2Base.Models;

namespace Core2Base.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        // showing the about us
        public IActionResult AboutUs()
        {
            return View();
        }

        // login user with email and password
        public IActionResult Login(string email, string password)
        {
            return View();
        }

        // forgot password
        public IActionResult ForgotPassword(string email, string newPassword)
        {
            return View();
        }

        // SignUp function with email and password
        public IActionResult SignUp(string email, string password)
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
