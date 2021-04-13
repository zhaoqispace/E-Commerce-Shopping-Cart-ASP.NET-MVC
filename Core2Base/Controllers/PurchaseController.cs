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
    public class PurchaseController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Product> ProductList = ProductData.GetProductInfo();
            ViewData["Products"] = ProductList;
            return View();
        }

        // Retrieving total price  
        public IActionResult ViewTotalPrice()
        {
            return View();
        }
    }
}
