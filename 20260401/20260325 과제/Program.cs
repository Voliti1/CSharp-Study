using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolApp1
{
    internal class CodeFile5
    {
        static void Main(string[] args)
        {
            string hakbun = ""; string name = ""; string age = ""; string cellphone = ""; string addr = "";
            uint sub1 = 0; uint sub2 = 0; uint sub3 = 0; uint total = 0; uint avg = 0;

            Console.Write("학번을 입력해주세요 : ");
            hakbun = Console.ReadLine();

            Console.Write("이름을 입력해주세요 : ");
            name = Console.ReadLine();

            Console.Write("나이를 입력해주세요 : ");
            age = Console.ReadLine();

            Console.Write("전화번호를 입력해주세요 : ");
            cellphone = Console.ReadLine();

            Console.Write("주소를 입력해주세요 : ");
            addr = Console.ReadLine();

            Console.Write("국어 성적을 입력해주세요 : ");
            sub1 = uint.Parse(Console.ReadLine());

            Console.Write("수학 성적을 입력해주세요 : ");
            sub2 = uint.Parse(Console.ReadLine());

            Console.Write("영어 성적을 입력해주세요 : ");
            sub3 = uint.Parse(Console.ReadLine());

            total = sub1 + sub2 + sub3;
            avg = total / 3 ;

            Console.Clear(); //콘솔화면 지우기
            Console.WriteLine("당신의 정보");
            Console.WriteLine("학번 : " + hakbun);
            Console.WriteLine("이름 : " + name);
            Console.WriteLine("나이 : " + age);
            Console.WriteLine("전화번호 : " + cellphone);
            Console.WriteLine("주소 : " + addr);
            Console.WriteLine("국어 : " + sub1 + "점");
            Console.WriteLine("수학 : " + sub2 + "점");
            Console.WriteLine("영어 : " + sub3 + "점");
            Console.WriteLine("합계 : " + total + "점");
            Console.WriteLine("평균 : " + avg + "점");
        }
    }
}