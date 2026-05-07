using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20260415_20260506
{
    internal class List_And_Math
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int> { 52, 273, 32, 64 };

            int count = 0;
            if (list.Count != 0)
            {
                foreach (var item in list)
                {
                    count += 1;
                    Console.WriteLine("Count : " + count + "\t item : " + item);
                }
            }
            else
            {
                Console.WriteLine("list에 데이터가 없습니다.");
            }

            Console.WriteLine();
            list.RemoveAll(n => n < 50);
            count = 0;
            if (list.Count != 0)
            {
                foreach (var item in list)
                {
                    count += 1;
                    Console.WriteLine("Count : " + count + "\t item : " + item);
                }
            }
            else
            {
                Console.WriteLine("list에 데이터가 없습니다.");
            }
            Console.WriteLine();

            list.Clear();
            count = 0;
            if(list.Count != 0)
            {
                foreach (var item in list)
                {
                    count += 1;
                    Console.WriteLine("Count : " + count + "\t item : " + item);
                }
            }
            else
            {
                Console.WriteLine("list에 데이터가 없습니다.");
            }
                
            Console.WriteLine();
            Console.WriteLine("Abs : "+ Math.Abs(-52273));
            Console.WriteLine("Ceiling : " + Math.Ceiling(52.273));
            Console.WriteLine("Floor : " + Math.Floor(52.273));
            Console.WriteLine("Max : " + Math.Max(52, 273));
            Console.WriteLine("Min : " + Math.Min(52, 273));
            Console.WriteLine("Round : " + Math.Round(52.273));
        }
    }
}


