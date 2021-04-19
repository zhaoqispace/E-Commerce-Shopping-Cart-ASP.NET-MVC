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
using Microsoft.AspNetCore.Http;
using X.PagedList.Mvc.Core;
using X.PagedList;

namespace Core2Base.Controllers
{
    public class PurchaseController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index(int? page)
        {
            //getting purchase history
            string UserID = HttpContext.Session.GetString("UserID");
            if (UserID != null)
            {
                List<PurchaseHistory> purchaseHistoryList = PurchaseHistoryData.GetPurchaseHistory(UserID);
                ViewData["PurchaseHistory"] = purchaseHistoryList;

                var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
                var onePageOfProducts = purchaseHistoryList.ToPagedList(pageNumber, 3); // will only contain 25 products max because of the pageSize

                ViewBag.OnePageOfProducts = onePageOfProducts;

            }
            ViewData["firstname"] = HttpContext.Session.GetString("firstname");
            return View();
        }

        // Retrieving activation status  
        public IActionResult GetActivationStatus([FromBody] ProductDate pDate)
        {
            Debug.WriteLine("Date", pDate.Date);
            List<string> activationCode = new List<string> { };
            Debug.WriteLine("ProductID", pDate.ProductID);
            string newFormat = DateTime.ParseExact(pDate.Date, "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
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
