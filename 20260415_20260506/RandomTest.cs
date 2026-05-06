using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20260415_20260506
{
    internal class RandomTest
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            Console.WriteLine(random.Next(10));
            Console.WriteLine(random.Next(10, 100));
            Console.WriteLine(random.Next(10, 100));
            Console.WriteLine(random.Next(10, 100));
            Console.WriteLine(random.Next(10, 100));
            Console.WriteLine(random.NextDouble());
        }
    }
}
