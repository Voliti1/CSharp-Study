using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20260408
{
    internal class For
    {
        static void Main(string[] args)
        {
            //for(int i = '가'; i <= '힣'; i++)
            //{
            //    Console.Write((char)i);
            //}

            //int[] intArray = { 52, 273, 32, 65, 103 };

            //for(int i = 0; i < intArray.Length; i++)
            //{
            //    Console.WriteLine(intArray[i]);
            //}
            //Console.WriteLine();
            //for (int i = intArray.Length - 1; i >= 0; i--)
            //{
            //    Console.WriteLine(intArray[i]);
            //}

            //string[] array = { "사과", "배", "포도", "딸기", "바나나"};

            //foreach(string item in array)
            //{
            //    Console.WriteLine(item);
            //}

            int count = 0;
            for(int i = 0; i < 10; i++)
            {
                for (int j = 0; j < i + 1; j++)
                {
                    Console.Write('*');
                    count++;
                }
                Console.WriteLine();
            }
            Console.WriteLine(count);
        }
    }
}
