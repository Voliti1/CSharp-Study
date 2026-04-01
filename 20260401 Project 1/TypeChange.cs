using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20260401
{
    internal class TypeChange
    {
        static void Main(string[] args)
        {
            //long longNumber = 2147483647L + 2147483647L;
            //int longToInt = (int)longNumber;
            //Console.WriteLine(longToInt);

            string numberString = "52273";
            int intNumber = int.Parse(numberString);
            Console.WriteLine(intNumber);
            //Console.WriteLine(int.Parse("52.273")); 
            //코드에서는 오류가 없다고 하지만 실행 시 오류
            Console.WriteLine(float.Parse("52.273"));
            //Console.WriteLine(int.Parse("2147483649"));
            //코드에서는 오류가 없다고 하지만실행 시 오류
            Console.WriteLine((52.273).ToString());

            unchecked
            {
                Console.WriteLine(-(-2147483648));
            }
        }
    }
}