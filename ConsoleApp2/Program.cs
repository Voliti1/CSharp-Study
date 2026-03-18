using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int sum = 0;
            for (int i = 0; i <= 100; i++)
            {
                sum += i;
            }

            Console.WriteLine("1부터 100까지 더한 값은 : " + sum);
        }
    }
}
