using System;
using FackerProgram;

namespace Faker
{
    class Program
    {
        static void Main(string[] args)
        {
            FackerProgram.Faker faker = new FackerProgram.Faker();
            MyTestClass test;
            ConsolePrinter printer;
            test = faker.Create<MyTestClass>();
            Console.WriteLine();
            printer = new ConsolePrinter();
            printer.DTOListAdd(typeof(Foo));
            printer.DTOListAdd(typeof(Bar));
            printer.Print(test,"");
        }
    }
}
