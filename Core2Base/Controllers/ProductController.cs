using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core2Base.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Core2Base.Controllers
{
    public class ProductController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Product> ProductList = ProductData.GetProductInfo();
            // use dummy data to show product details
            //ProductList.Add(new Product { Id = 1, Name = "Game 1", Description = "This is a wonderful game which is designed for people above age 11.", UnitPrice = 11.5, Image = "/productimages/1.jpg" });
            //ProductList.Add(new Product { Id = 2, Name = "Game 2", Description = "This is a wonderful game which is designed for people above age 12.", UnitPrice = 12.5, Image = "/productimages/2.png" });
            //ProductList.Add(new Product { Id = 3, Name = "Game 3", Description = "This is a wonderful game which is designed for people above age 13.", UnitPrice = 13.5, Image = "/productimages/3.jpg" });
            //ProductList.Add(new Product { Id = 4, Name = "Game 4", Description = "This is a wonderful game which is designed for people above age 14.", UnitPrice = 14.5, Image = "/productimages/4.png" });
            //ProductList.Add(new Product { Id = 5, Name = "Game 5", Description = "This is a wonderful game which is designed for people above age 15.", UnitPrice = 15.5, Image = "/productimages/5.jpg" });
            //ProductList.Add(new Product { Id = 6, Name = "Game 6", Description = "This is a wonderful game which is designed for people above age 16.", UnitPrice = 16.5, Image = "/productimages/6.png" });
            //ProductList.Add(new Product { Id = 7, Name = "Game 7", Description = "This is a wonderful game which is designed for people above age 17.", UnitPrice = 17.5, Image = "/productimages/7.jpg" });
            //ProductList.Add(new Product { Id = 8, Name = "Game 8", Description = "This is a wonderful game which is designed for people above age 18.", UnitPrice = 18.5, Image = "/productimages/8.jpg" });
            //ProductList.Add(new Product { Id = 9, Name = "Game 9", Description = "This is a wonderful game which is designed for people above age 19.", UnitPrice = 19.5, Image = "/productimages/9.jpg" });
            //ProductList.Add(new Product { Id = 10, Name = "Game 10", Description = "This is a wonderful game which is designed for people above age 20.", UnitPrice = 20.5, Image = "/productimages/10.png" });
            //ProductList.Add(new Product { Id = 11, Name = "Game 11", Description = "This is a wonderful game which is designed for people above age 21.", UnitPrice = 21.5, Image = "/productimages/11.jpg" });


            // this ViewData key-value pair is to pass data from Controller to View
            ViewData["Products"] = ProductList;
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
