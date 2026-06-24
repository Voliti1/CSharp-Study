using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20260624
{
    class Parent
    {
        public Parent() { Console.WriteLine("Parent()"); }
        public Parent(int Param) { Console.WriteLine("Parent(int Param)"); }
        public Parent(string param) { Console.WriteLine("Parent(string Param)"); }
    }
    class Child : Parent
    {
        public Child() : base(10)
        {
            Console.WriteLine("Child() : base(10)");
        }

        public Child(string input) : base(input)
        {
            Console.WriteLine("Child() : base(input)");
        }
    }
    
    internal class Parents
    {
        static void Main(string[] args)
        {
            Child childA = new Child();
            Child childB = new Child("string");
        }
    }
}
