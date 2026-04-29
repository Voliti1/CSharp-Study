using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM_Stock_Management_System
{
    internal class Product
    {
        public string Product_id { get; set; }
        public string Product_name { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public string ExpirationDate { get; set; } //유통기한
        public string Classification { get; set; }

    }
}
