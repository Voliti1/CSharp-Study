using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20260408
{
    internal class Array
    {
        static void Main(string[] args)
        {
            int i = 0;
            int[] intArray = { 52, 273, 32, 65, 103 };

            //intArray[0] = 0;

            //Console.WriteLine(intArray[0]);
            //Console.WriteLine(intArray[1]);
            //Console.WriteLine(intArray[2]);
            //Console.WriteLine(intArray[3]);
            //Console.WriteLine(intArray[4]);

            while (i < intArray.Length)
            {
                Console.WriteLine($"{i}번째 출력 : {intArray[i]}");
                i++;
            }

            string input;
            do
            {
                Console.Write("입력(exit를 입력하면 종료) : ");
                input = Console.ReadLine();
            } while (input != "exit");

        }
    }
}
