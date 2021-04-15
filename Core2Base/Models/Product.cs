using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core2Base.Models
{
    public class Product
    {
        public Guid Id { get; set; } // better to use string type
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        [DataType(DataType.Currency)]  // to show dollar sign
        public double UnitPrice { get; set; }
        public string Image { get; set; }
    }
}
