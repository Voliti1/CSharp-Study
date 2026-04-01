using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20260401
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("학점을 입력해 주세요 : ");
            double score = double.Parse(Console.ReadLine());

            if (score == 4.5)
                Console.WriteLine("A+");
            else if (4.2 <= score && score < 4.5)
                Console.WriteLine("A0");
            else if (3.5 <= score && score < 4.2)
                Console.WriteLine("B+");
            else if (2.8 <= score && score < 3.5)
                Console.WriteLine("B");
            else if (2.3 <= score && score < 2.8)
                Console.WriteLine("C+");
            else if (1.75 <= score && score < 2.3)
                Console.WriteLine("C0");
            else if (1.0 <= score && score < 1.75)
                Console.WriteLine("D+");
            else if (0.5 <= score && score < 1.0)
                Console.WriteLine("D0");
            else if (0 < score && score < 0.5)
                Console.WriteLine("E");
            else
                Console.WriteLine("F");
        }
    }
}
