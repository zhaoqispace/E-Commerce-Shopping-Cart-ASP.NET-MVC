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
using X.PagedList.Mvc.Core;
using X.PagedList;


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
                ViewData["numberofcartitems"]= CartData.NumberOfCartItems(UserID);
                ViewData["firstname"] = HttpContext.Session.GetString("firstname");
                return View();
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
            List<CartDetail> cartinfo2 = new List<CartDetail>();

            if (UserID != null)
            {
                //add to cart in DB for logged in user
                //List<CartDetail> usercart = CartData.GetCartInfo(UserID);

                cartinfo2 = CartData.GetCartInfo(UserID);
                var iter2 = from cartitem in cartinfo2 where cartitem.ProductId == productid.ProductId select cartitem;
                foreach (var productincart2 in iter2)
                {
                    if (productincart2.qty >= 99)
                    {
                        return Json(new { success = false });
                    }
                    else
                    {
                        int success = CartData.AddProductToCart(UserID, productid.ProductId);

                        return Json(new { success = true });
                    }
                }

            }
            else
            {



            }
            return Json(new { success = true });
        }
        [HttpPost]
        public JsonResult SubtractProductFromCart([FromBody] CartDetail productid, CartDetail user)
        {
            string UserID = HttpContext.Session.GetString("UserID");

            List<CartDetail> cartinfo = new List<CartDetail>();


            if (UserID != null)
            {
                cartinfo = CartData.GetCartInfo(UserID);
                var iter = from cartitem in cartinfo where cartitem.ProductId == productid.ProductId select cartitem;
                foreach (var productincart in iter)
                {
                    if (productincart.qty <= 1)
                    {
                        return Json(new { success = false });
                    }
                    else
                    {
                        int success = CartData.SubtractProductFromCart(UserID, productid.ProductId);
                        return Json(new { success = true });
                    }
                }

            }
            else
            {

            }
            return Json(new { success = true });
        }
    }
    
}
