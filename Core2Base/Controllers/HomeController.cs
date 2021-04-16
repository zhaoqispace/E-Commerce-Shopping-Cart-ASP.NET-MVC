using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Core2Base.Models;
using Core2Base.Data;
using BC = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Http;

namespace Core2Base.Controllers
{
    public class HomeController : Controller
    {
        private readonly Session session;
        private readonly ILogger<HomeController> _logger;
        //combine product controller with home controller
        //test commit
        public HomeController(ILogger<HomeController> logger, Session session)
        {
            _logger = logger;
            this.session = session;
        }
        public IActionResult Index()
        {
            /*
            if (Request.Cookies["sessionID"] == null)
            {
                Response.Cookies.Append("sessionID", session.SessionID);
            }
            */
            HttpContext.Session.SetString("sessionid", session.SessionID);
            
            // this ViewData key-value pair is to pass data from Controller to View
            List<Product> ProductList = ProductData.GetProductInfo();
            ViewData["Products"] = ProductList;
            return View();
        }
        
        //Search Results method and page
        public IActionResult SearchResults(string searchTerm)
        {
            if (searchTerm != null)
            {
                searchTerm = searchTerm.Trim();
            }
            List<Product> foundProducts = ProductData.SearchProducts(searchTerm);

            ViewData["foundProducts"] = foundProducts;
            return View(foundProducts);
        }


        // Retrieving products 
        public IActionResult ShowProductLists()
        {
           return View();
        }

        //Searching product with specific name
        public IActionResult SearchProduct()
        {
            return View();
        }

        // showing the about us
        public IActionResult About()
        {
            return View();
        }

        // login user with email and password
        public IActionResult Login(string email, string password)
        {

            if (email == null && password != null)
            {
                ViewData["errMsg"] = "Email field must be filled in.";
                return View();
            }
            else if (password == null && email != null)
            {
                ViewData["errMsg"] = "Password field must be filled in.";
                return View();
            }

            else if (password != null && email != null)
            {
                User user = UserData.GetUserInfo(email);

                if (user != null && BC.Verify(password, user.Password))
                {
                    TempData["userinsession"] = user.Email;
                    HttpContext.Session.SetString("email", email);
                    HttpContext.Session.SetString("UserID", Convert.ToString(user.UserId));
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["errMsg"] = "You have entered an incorrect email or password. Please try again";
                    return View();
                }
            }
            return View();
        }

        // forgot password
        public IActionResult ForgotPassword(string email, string newPassword)
        {
            return View();
        }

        // SignUp function with email and password
        public IActionResult SignUp()
        {
                return View();
        }

        [HttpPost]
        public IActionResult GetDetails()
        {
            User model = new User
            {
                FirstName = HttpContext.Request.Form["firstname"].ToString(),
                LastName = HttpContext.Request.Form["lastname"].ToString(),
                Gender = HttpContext.Request.Form["gender"].ToString(),
                Email = HttpContext.Request.Form["email"].ToString(),
                Password = HttpContext.Request.Form["password"].ToString(),
                Salutation = HttpContext.Request.Form["salutations"].ToString(),
                Address = HttpContext.Request.Form["address"].ToString()
            };

            int result = model.SaveDetails();
            if (result > 0)
            {
                ViewData["Result"] = "Thank you! You have successfully signed in :)";
            }
            else
            {
                ViewData["Result"] = "Something Went Wrong";
            }
            return View("SignUp");
        }

        // showing the about us
        public IActionResult AboutUs()
        {
            return View();
        }

       public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Response.Cookies.Delete(".AspNetCore.Session");
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}
