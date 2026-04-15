using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20260415
{
    internal class List_And_Math
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int> { 52, 273, 32, 64 };

            int count = 0;
            foreach(var item in list)
            {
                count += 1;
                Console.WriteLine("Count : " + count + "\t item : " + item);
            }

            Console.WriteLine();
            Console.WriteLine("Abs : "+ Math.Abs(-5220));
            Console.WriteLine("Ceiling : " + Math.Ceiling(31.41592));
            Console.WriteLine("Floor : " + Math.Floor(31.41592));
            Console.WriteLine("Max : " + Math.Max(10, 100));
            Console.WriteLine("Min : " + Math.Min(10, 100));
            Console.WriteLine("Round : " + Math.Round(52.273));
        }
    }
}


