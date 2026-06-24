using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20260624
{
    class Animal
    {
        public int Age { get; set; }
        public Animal() { this.Age = 0; }

        public virtual void Eat() { Console.WriteLine("냠냠 먹습니다."); }
        public void Sleep() { Console.WriteLine("쿨쿨 잠을 잡니다."); }
    }
    class Dog : Animal
    {
        public string Color { get; set; }
        public Dog() { this.Age = 0; }

        public override void Eat() { Console.WriteLine("강아지 사료를 먹습니다."); }
        public void Bark() { Console.WriteLine("왈왈 짖습니다."); }
    }

    class Cat : Animal
    {
        public Cat() { this.Age = 0; }

        public override void Eat() { Console.WriteLine("고양이 사료를 먹습니다."); }
        public void Meow() { Console.WriteLine("냥냥 웁니다."); }
    }
    internal class Animals
    {
        static void Main(string[] args)
        {
            List<Animal> Animals = new List<Animal>() 
            { 
                new Dog(), new Cat(), new Dog(),
                new Cat(), new Dog(), new Cat()
            };


            foreach (var item in Animals)
            {
                item.Eat();
                //item.Sleep();

                //if (item is Dog) { (item as Dog).Bark(); }
                //if (item is Cat) { (item as Cat).Meow(); }

                //var dog = item as Dog;
                //if (dog != null) { dog.Bark(); }
                //var cat = item as Cat;
                //if (cat != null) { cat.Meow(); }
            }
        }
    }
}
