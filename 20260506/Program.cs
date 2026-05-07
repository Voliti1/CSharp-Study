using System;
using System.Collections.Generic;
using System.Linq;

namespace _20260506
{
    class Student
    {
        public string id;
        public string name;
        public int grade;
        public string major;
        public DateTime birthday;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<Student> list = new List<Student>();
            list.Add(new Student() { name = "윤인성", grade = 1 });
            list.Add(new Student() { name = "연하진", grade = 2 });
            list.Add(new Student() { name = "윤아린", grade = 3 });
            list.Add(new Student() { name = "윤명월", grade = 4 });
            list.Add(new Student() { name = "구지연", grade = 1 });
            list.Add(new Student() { name = "김연화", grade = 2 });

            foreach(var item in list)
            {
                Console.WriteLine(item.name + " : " + item.grade);
            }

            foreach (var item in list.ToList())
            {
                if (item.grade > 1)
                {
                    list.Remove(item); // 원본 리스트(list)에서 삭제해도 괜찮음
                }
            }

            Console.WriteLine();

            foreach (var item in list)
            {
                Console.WriteLine(item.name + " : " + item.grade);
            }
        }
    }
}
