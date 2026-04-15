using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20260415
{
    internal class Program
    {
        class Product
        {
            public string name;
            public int price;
        }
        static void Main(string[] args)
        {
            Product product1 = new Product() { name = "감자", price = 2000 };

            Console.WriteLine(product1.name + " : " + product1.price + "원");
        }
    }
}