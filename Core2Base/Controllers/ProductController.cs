using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core2Base.Models;
using Microsoft.AspNetCore.Mvc;
using Core2Base.Data;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Core2Base.Controllers
{
    public class ProductController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Product> ProductList = new List<Product>();

            return View();
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

    }
}
