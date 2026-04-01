using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int result = 0;
            Console.WriteLine("Hello C# World");
            
            if(args.Length == 0)
            {
                Console.WriteLine("입력 매개 변수가 없습니다.");
            }

            if (args.Length == 1)
            {
                Console.WriteLine("입력 매개 변수가 있습니다.");
                Console.WriteLine("매개변수 값 : " + args[0]);

                if (args[0] == "/add")
                {
                    for(int i = 0; i <= 10; i++)
                    {
                        result += i;
                    }
                    Console.WriteLine("1부터 10까지의 합은 : " + result);
                }
            }

            Console.ReadLine();
        }
    }
}
