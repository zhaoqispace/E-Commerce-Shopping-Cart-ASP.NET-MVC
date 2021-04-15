using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core2Base.Models
{
    public class PurchaseHistory
    {
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int Quantity { get; set; }
        public List<string> ActivationStatus { get; set; }
        public double Price { get; set; }
        public List<string> DateOfPurchase { get ; set; }
    }
}
