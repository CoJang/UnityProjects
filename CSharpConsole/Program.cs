using System;

namespace CSharpConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            double a = 20.5f;
            object b = (int)a;

            Console.WriteLine(b);

            int? c = null;
            Console.WriteLine(c ?? 0);

            if (c.HasValue)
            {
                Console.WriteLine(c);
            }
            else
            {
                Console.WriteLine("NULL");
            }

            var d = 3.141592;
            Console.WriteLine("Type: {0}, Value: {1}", d.GetType(), d);

            var f = new int[] { 10, 20, 30 };
            Console.WriteLine("Type: {0}", f.GetType());

            Mammal mammal = new Mammal();
            mammal.Nurse();

            mammal = new Dog();
            mammal.Nurse();

            Dog dog = new Dog();
            dog.Nurse();
            dog.Bark();

            mammal = new Cat();
            mammal.Nurse();

            Cat cat = (Cat)mammal;
            cat.Nurse();
            cat.Meow();
        }
    }

    class Mammal
    {
        public void Nurse() { Console.WriteLine("Nurse"); }
    }

    class Dog : Mammal
    {
        public void Bark() { Console.WriteLine("Bark"); }
    }
    class Cat : Mammal
    {
        public void Meow() { Console.WriteLine("Meow"); }
    }
}
