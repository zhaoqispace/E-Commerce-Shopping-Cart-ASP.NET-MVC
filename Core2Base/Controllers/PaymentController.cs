using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Core2Base.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Core2Base.Controllers
{
    public class PaymentController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
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
            }
            return View();
        }


        // retrieving payment history
        public IActionResult ViewHistory()
        {
            return View();
        }

        // retrieving alternative page, which is catered for returning users

        public IActionResult Alternate()
        {
            return View();
        }

    }
}
