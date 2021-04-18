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
using X.PagedList.Mvc.Core;
using X.PagedList;


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
        public IActionResult Index(int? page)
        {
            /*
            if (Request.Cookies["sessionID"] == null)
            {
                Response.Cookies.Append("sessionID", session.SessionID);
            }
            */
            HttpContext.Session.SetString("sessionid", session.SessionID);
            
            //List<Product> ProductList = ProductData.GetProductInfo();
            //ViewData["userinsession"] = TempData["userinsession"];

            List<Product> ProductList = ProductData.GetProductInfo();
            ViewData["firstname"] = HttpContext.Session.GetString("firstname");
            HttpContext.Session.SetString("sessionid", session.SessionID);
            ViewData["Products"] = ProductList;

            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfProducts = ProductList.ToPagedList(pageNumber, 6); // will only contain 25 products max because of the pageSize

            ViewBag.OnePageOfProducts = onePageOfProducts;

            if (HttpContext.Session.GetString("UserID")!= null)
            {
                ViewData["qtyInCart"] = CartData.NumberOfCartItems(HttpContext.Session.GetString("UserID"));
            }
            else
            {
                ViewData["qtyInCart"] = CartData.NumberOfCartItemsTemp(HttpContext.Session.GetString("sessionid"));
            }
            return View();
        }
        
        //Search Results method and page
        public IActionResult SearchResults(string searchTerm, int? page)
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                ViewData["qtyInCart"] = CartData.NumberOfCartItems(HttpContext.Session.GetString("UserID"));
            }
            else
            {
                ViewData["qtyInCart"] = CartData.NumberOfCartItemsTemp(HttpContext.Session.GetString("sessionid"));
            }
            if (searchTerm != null)
            {
                //Remove all leading and trailing white-spaces
                searchTerm = searchTerm.Trim();

                // to enable searchTerm with "$"
                if (searchTerm.StartsWith('$'))
                {
                    searchTerm = (searchTerm as string).Trim('$');
                }

                // to enable searchTerm with 
                if (searchTerm.Contains("."))
                {
                    //Remove all trailing zeros
                    searchTerm = searchTerm.TrimEnd('0');

                    //If all we are left with is a decimal point,then remove it
                    if (searchTerm.EndsWith("."))
                        searchTerm = searchTerm.TrimEnd('.');
                }

          
            }

            
            List<Product> foundProducts = ProductData.SearchProducts(searchTerm);

            ViewData["foundProducts"] = foundProducts;
            ViewData["firstname"] = HttpContext.Session.GetString("firstname");

            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfProducts = foundProducts.ToPagedList(pageNumber, 6); // will only contain 25 products max because of the pageSize

            ViewBag.OnePageOfProducts = onePageOfProducts;

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
            if (HttpContext.Session.GetString("UserID") != null)
            {
                ViewData["qtyInCart"] = CartData.NumberOfCartItems(HttpContext.Session.GetString("UserID"));
            }
            else
            {
                ViewData["qtyInCart"] = CartData.NumberOfCartItemsTemp(HttpContext.Session.GetString("sessionid"));
            }
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
                    HttpContext.Session.SetString("UserID", Convert.ToString(user.UserId));
                    HttpContext.Session.SetString("firstname", user.FirstName);
                    int sucess = CartData.MergeTempCartAndDelete(HttpContext.Session.GetString("UserID"), HttpContext.Session.GetString("sessionid"));
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
                Password = BC.HashPassword(HttpContext.Request.Form["password"].ToString()),
                DateOfBirth = Convert.ToString(HttpContext.Request.Form["DOB"]),
                Salutation = HttpContext.Request.Form["salutations"].ToString(),
                Address = HttpContext.Request.Form["address"].ToString(),
                PostalCode = HttpContext.Request.Form["postalcode"].ToString()

            };

            int result = model.SaveDetails();
            if (result > 0)
            {
                ViewData["Result"] = "Thank you! You have successfully signed in :)";
            }
            else
            {
                ViewData["Result"] = "Sorry this email is already registered";
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