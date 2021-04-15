using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core2Base.Models
{
    public class CartDetail
    {
        public int CartId { get; set; }
        public Guid ProductId { get; set; }
        public int qty { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public string ProductCat { get; set; }
        [DataType(DataType.Currency)]  // to show dollar sign
        public double UnitPrice { get; set; }
        public string ProductImg { get; set; }
    }
}
