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
            int sum = 0; //결과값 누적을 위한 변수 선언
            for (int i = 0; i <= 100; i++) // 1부터 100까지 반복
            {
                //sum = sum + i와 같음. 누적합을 구하는 부분
                sum += i;
            }

            Console.WriteLine("1부터 100까지 더한 값은 : " + sum); //결과값 출력

            /* 새로운 챕터 
             * Console.Write와 
             * Console.WriteLine을
             * 비교하기 위한 코드*/
            Console.Write("Write ");
            Console.Write("Write ");
            Console.Write("Write ");
            Console.WriteLine("Write");
            Console.WriteLine("Write");
            Console.WriteLine("Write");

            string hello = "안녕하세요";

            Console.WriteLine(hello[0]);
            Console.WriteLine(hello[2]);
            Console.WriteLine(hello[4]);

            bool a = true;
            bool b = false;
            Console.WriteLine(a || b);
            Console.WriteLine(a && b);

        }
    }
}
