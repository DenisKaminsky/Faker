using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FackerProgram;
using System.Runtime.InteropServices;

namespace Faker
{
    class Program
    {
        //генератор Char
        public static double GenerateChar()
        {
            Random rand = new Random();
            return rand.NextDouble();
        }

        static void Main(string[] args)
        {
            FackerProgram.Faker faker = new FackerProgram.Faker();
            faker.DTOAdd(typeof(Foo));     
            MyTestClass c = faker.Create<MyTestClass>();
            ConsolePrinter printer = new ConsolePrinter();
            printer.Print(c);
        }
    }

    public class MyTestClass
    {
        //public bool[] mass;
       // public List<int>[] list;
        public Foo foo;
        private int fieldprivate;
        public int fieldpublic;
        
        public short fieldpublic2;
        public double fieldpublic8;
        public string s;
        public DateTime d;

        public string Str { get; set; }
        private MyTestClass proverty2 { get; set;}

        public MyTestClass(int f1, short f2, string f3)
        {
            fieldpublic = f1;
            fieldpublic2 = f2;
            Str = f3;
        }

        public MyTestClass()
        {

        }
    }

    public class Foo
    {        
        private int fieldprivate;
        public bool fieldpublic;

        public short fieldpublic2;
        public DateTime d;

        public string Str { get; set; }

        public Foo()
        {

        }
    }

}
