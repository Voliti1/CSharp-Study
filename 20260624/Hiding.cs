using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20260624
{
    class Parent1
    {
        public int variable = 273;
        public void Method()
        {
            Console.WriteLine("부모의 메서드");
        }
    }

    class Child1 : Parent1
    {
        public new string variable = "shadowing";
        public new void Method()
        {
            Console.WriteLine("자식의 메서드");
        }
    }
    internal class Hiding
    {
        static void Main(string[] args)
        {
            Child1 child1 = new Child1();
            child1.Method();
            ((Parent1)child1).Method();
        }
    }
}
