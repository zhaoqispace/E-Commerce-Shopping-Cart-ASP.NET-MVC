using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Core2Base.Models;
using Core2Base.Data;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Core2Base.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            string UserID = HttpContext.Session.GetString("UserID");
            if (UserID != null)
            {
                List<CartDetail> usercart = CartData.GetCartInfo(UserID);
                ViewData["usercart"] = usercart;
            }
            else
            {
                //List<CartDetail> usercart = 

            }

            //List<Product> ProductList = ProductData.GetProductInfo();
            //ViewData["Products"] = ProductList;
            return View();
        }

        // Adding product to shopping cart
        [HttpPost]
        public JsonResult AddToCart([FromBody] CartDetail productid)
        {

            string UserID = HttpContext.Session.GetString("UserID");

            if (UserID != null)
            {
                //add to cart in DB for logged in user
                //List<CartDetail> usercart = CartData.GetCartInfo(UserID);

                int success = CartData.AddProductToCart(UserID, productid.ProductId);

                return Json(new { success = true });
            }
            else
            {



            }
            return Json(new { success = true });

        }
    }
}
