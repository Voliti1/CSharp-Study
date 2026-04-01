using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20260401_task
{
    internal class JunwonSeo_20260401_Task
    {
        static void Main(string[] args)
        {
            int ameprice = 2000;
            int latteprice = 3000;
            int teaprice = 2500;

            int totalnum = 0;
            int sumprice = 0;
            int saleprice = 0;
            int totalprice = 0;
            float salerate = 0f;

            Console.Write("아메리카노 주문 잔수를 입력해주세요 : ");
            int amenum = int.Parse(Console.ReadLine());
            Console.Write("라떼 주문 잔수를 입력해주세요 : ");
            int lattenum = int.Parse(Console.ReadLine());
            Console.Write("녹차 주문 잔수를 입력해주세요 : ");
            int teanum = int.Parse(Console.ReadLine());
            totalnum = amenum + lattenum + teanum;
            sumprice = ameprice * amenum + latteprice * lattenum + teaprice * teanum;

            if (sumprice >= 20000)
            {
                saleprice = (int)(sumprice * 0.2);
                salerate = 0.2f;
            }
            else if (sumprice < 20000 && sumprice >= 15000)
            {
                saleprice = (int)(sumprice * 0.15);
                salerate = 0.15f;
            }                
            else
                saleprice = 0;

            totalprice = sumprice - saleprice;
            
            Console.WriteLine("항목\t\t" + "잔수\t" + "금액");
            Console.WriteLine("아메리카노\t" + amenum + "잔\t" + ameprice * amenum + "원");
            Console.WriteLine("라떼\t\t" + lattenum + "잔\t" + latteprice * lattenum + "원");
            Console.WriteLine("녹차\t\t" + teanum + "잔\t" + teaprice * teanum + "원");
            Console.WriteLine("────────────────────────────────────────");
            Console.WriteLine("합계\t\t" + totalnum + "잔\t" + sumprice + "원");
            Console.WriteLine("할인금액\t" + (salerate * 100) + "%\t" + sumprice + "원");
            Console.WriteLine("지불금액\t\t" + totalprice + "원");

        }
    }
}
