using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Core2Base.Models;
using Core2Base.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Core2Base.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Product> ProductList = ProductData.GetProductInfo();
            ViewData["Products"] = ProductList;
            return View();
        }

        // Adding product to shopping cart
        public IActionResult AddProductToCart()
        {
            return View();
        }
    }
}
