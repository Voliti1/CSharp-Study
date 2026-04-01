using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20260401
{
    internal class Switch_case
    {
        static void Main(string[] args)
        {
            //Console.Write("숫자를 입력하세요 : ");
            //int input = int.Parse(Console.ReadLine());

            //switch (input % 2)
            //{
            //    case 0:
            //        Console.WriteLine("짝수");
            //        break;

            //    case 1:
            //        Console.WriteLine("홀수");
            //        break;
            //}
            Console.Write("몇 월인지 입력하세요 : ");
            int month = int.Parse(Console.ReadLine());
            switch (month)
            {
                case 12:
                case 1:
                case 2:
                    Console.WriteLine("겨울입니다.");
                    break;
                case 3:
                case 4:
                case 5:
                    Console.WriteLine("봄입니다.");
                    break;
                case 6:
                case 7:
                case 8:
                    Console.WriteLine("여름입니다.");
                    break;
                case 9:
                case 10:
                case 11:
                    Console.WriteLine("가을입니다.");
                    break;
                default:
                    Console.WriteLine("1월부터 12월 사이의 값만 입력해주세요.");
                    break;
            }
        }
    }
}