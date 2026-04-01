using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20260401
{
    internal class If_else
    {
        static void Main(string[] args)
        {
            //Console.Write("숫자 입력 : ");
            //int input = int.Parse(Console.ReadLine());

            //if (input % 2 == 0)
            //{
            //    Console.WriteLine("짝수입니다!");
            //}
            //else 
            //{
            //    Console.WriteLine("홀수입니다!");
            //}
            //if (input % 2 == 1)
            //{
            //    Console.WriteLine("홀수입니다!");
            //}



            //String ampm = "";
            //int hour = 0;

            //if ( DateTime.Now.Hour < 12)
            //{
            //    ampm = "AM";
            //    hour = DateTime.Now.Hour;
            //}
            //if (DateTime.Now.Hour >= 12)
            //{
            //    ampm = "PM";
            //    hour = DateTime.Now.Hour - 12;
            //}

            if(DateTime.Now.Hour < 12)
            {
                Console.WriteLine("오전 수업 시간입니다.");
            }
            else
            {
                if(DateTime.Now.Hour <= 18)
                {
                    Console.WriteLine("오후 수업 중입니다.");
                }
                else
                {
                    Console.WriteLine("오후 수업 종료되었습니다.");
                }
            }

            //Console.WriteLine(DateTime.Now.Year + "년 " + DateTime.Now.Month + "월 " + DateTime.Now.Day + "일 " + 
            //    ampm + " " + hour + "시 " + DateTime.Now.Minute + "분 " + DateTime.Now.Second + "초");
        }
    }
}
