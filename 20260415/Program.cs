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
            public string name { get; set; }
            public int price { get; set; }
        }

        static void Main(string[] args)
        {
            Product product1 = new Product() { name = "감자", price = 2000};
            Product product2 = new Product() { name = "고구마", price = 3000 };

            Console.WriteLine(product1.name + " : " + product1.price + "원");
            Console.WriteLine(product2.name + " : " + product2.price + "원");


        }
    }
}