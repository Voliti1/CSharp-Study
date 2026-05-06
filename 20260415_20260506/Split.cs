using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20260415_20260506
{
    internal class Split
    {
        static void Main(string[] args)
        {
            string input = "감자 고구마 토마토";
            string[] inputs = input.Split(new char[] { '고' });
            foreach(var item in inputs)
            {
                Console.WriteLine(item);
            }

            string input2 = "  test    ";
            Console.WriteLine("::" + input2.Trim() + "::");
            Console.WriteLine("::" + input2.TrimStart() + "::");
            Console.WriteLine("::" + input2.TrimEnd() + "::");
        }
    }
}
