using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20260408_Task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* 1 아메리카노 2000, 
             * 2 라떼 3000, 
             * 3 녹차 2500
             * 99 주문종료
             * 주문받기, 메뉴 선택
             * 잔수 입력
             * 금액 계산
             * 주문 종료 선택 시 영수증 출력
             * 주문내역
             * -------------------------
             * 항목 / 잔수 / 단가 / 합계
             * -------------------------
             * 합계 총 잔수, 총 합계
             * 지불 금액 입력 받고
             * 지불금액 고려해서 거스름돈 출력하기 
             * 지불금액 불만족시 만족 금액 입력시까지 입력받기
             * 상기금액을 영수합니다. 이용해 주셔서 감사합니다 출력*/
            int ameprice = 2000;
            int latteprice = 3000;
            int teaprice = 2500;
            int amenum = 0;
            int lattenum = 0;
            int teanum = 0;

            int menu= 0;
            int ordernum = 0;
            int totalnum = 0;
            int sumprice = 0;
            int payprice = 0;

            while(true) {
                Console.WriteLine("============메뉴판============");
                Console.WriteLine("1. 아메리카노\t 2000원");
                Console.WriteLine("2. 라떼\t\t 3000원");
                Console.WriteLine("3. 녹차\t\t 2500원");
                Console.WriteLine("99. 주문종료");
                Console.WriteLine("==============================");
                Console.Write("메뉴번호 : ");
                menu = int.Parse(Console.ReadLine());
                if (menu == 99)
                    break;
                Console.Write("잔 수 : ");
                ordernum = int.Parse(Console.ReadLine());
                if (menu == 1)
                {
                    amenum = ordernum;
                    Console.WriteLine($"아메리카노 {amenum}잔 주문되었습니다.");
                }
                else if (menu == 2)
                {
                    lattenum = ordernum;
                    Console.WriteLine($"라떼 {lattenum}잔 주문되었습니다.");
                }
                else if (menu == 3)
                {
                    teanum = ordernum;
                    Console.WriteLine($"녹차 {teanum}잔 주문되었습니다.");
                }
            }
            
            totalnum = amenum + lattenum + teanum;
            sumprice = ameprice * amenum + latteprice * lattenum + teaprice * teanum;

            while (payprice < sumprice)
            {
                Console.WriteLine("============영수증============");
                Console.WriteLine("항목\t\t" + "잔수\t" + "금액");
                Console.WriteLine("==============================");
                if(amenum > 0)
                    Console.WriteLine("아메리카노\t" + amenum + "잔\t" + ameprice * amenum + "원");
                if(lattenum > 0)
                    Console.WriteLine("라떼\t\t" + lattenum + "잔\t" + latteprice * lattenum + "원");
                if(teanum > 0)
                    Console.WriteLine("녹차\t\t" + teanum + "잔\t" + teaprice * teanum + "원");
                Console.WriteLine("==============================");
                Console.WriteLine("합계\t\t" + totalnum + "잔\t" + sumprice + "원");
                Console.Write("지불금액 : ");
                payprice = int.Parse(Console.ReadLine());
                if (payprice < sumprice)
                    Console.WriteLine("지불금액이 부족합니다. 다시 입력해주세요.");
                else
                {
                    Console.WriteLine($"거스름돈 : {payprice - sumprice}원");
                    Console.WriteLine("상기금액을 영수합니다.");
                    Console.WriteLine("이용해주셔서 감사합니다.");
                }
            } 

        }
    }
}
