using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Core2Base.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Core2Base.Controllers
{
    public class PaymentController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["firstname"] = HttpContext.Session.GetString("firstname");
            return View();
        }

        // Payment authorization upon Success
        public IActionResult Success()
        {
            string UserID = HttpContext.Session.GetString("UserID");
            string cardNumber = HttpContext.Request.Form["cardnumber"].ToString();
            Debug.WriteLine("UserID", UserID);
            Debug.WriteLine("Card Number", cardNumber);
            if (UserID != null)
            {
                int i = PaymentData.InsertCardInfo(cardNumber, UserID);
                PaymentData.InsertOrderDetails(UserID);
                PaymentData.DeleteOrderFromCart(UserID);
            }
            ViewData["firstname"] = HttpContext.Session.GetString("firstname");
            return View();
        }


        // retrieving payment history
        public IActionResult ViewHistory()
        {
            ViewData["firstname"] = HttpContext.Session.GetString("firstname");
            return View();
        }
    }
}
