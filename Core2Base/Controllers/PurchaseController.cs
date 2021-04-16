using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Core2Base.Data;
using Core2Base.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Core2Base.Models;
using Core2Base.Data;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Core2Base.Controllers
{
    public class PurchaseController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            //getting purchase history
            string UserID = HttpContext.Session.GetString("UserID");
            if (UserID != null)
            {
                List<PurchaseHistory> purchaseHistoryList = PurchaseHistoryData.GetPurchaseHistory(UserID);
                ViewData["PurchaseHistory"] = purchaseHistoryList;
            }

            return View();
        }

        // Retrieving activation status  
        public IActionResult GetActivationStatus([FromBody] ProductDate pDate)
        {
            string activationCode = "";
            Debug.WriteLine("Date", pDate.Date);
            Debug.WriteLine("ProductID", pDate.ProductID);
            string newFormat = DateTime.ParseExact(pDate.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-dd-MM", CultureInfo.InvariantCulture);
            Debug.WriteLine("New format", newFormat);
            string UserID = HttpContext.Session.GetString("UserID");
            if (UserID != null)
            {
                activationCode = PurchaseHistoryData.GetDateAndActivation(newFormat, pDate.ProductID, UserID);
                Debug.WriteLine(activationCode);

            }
            return Json(new
            {
                success = true,
                status = activationCode
            });
        }
    }
}
