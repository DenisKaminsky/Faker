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
        static void Main(string[] args)
        {
            FackerProgram.Faker faker = new FackerProgram.Faker();
            faker.DTOAdd(typeof(Foo));
            faker.DTOAdd(typeof(Bar));
            MyTestClass c = faker.Create<MyTestClass>();
            ConsolePrinter printer = new ConsolePrinter();
            printer.DTOListAdd(typeof(Foo));
            printer.DTOListAdd(typeof(Bar));
            printer.Print(c,"");
        }
    }

    public class MyTestClass
    {
        public bool[] mass;
        public List<int>[] list;
        public Foo foo;
        public Bar bar;
        public int fieldpublic;
        
        public short fieldpublic2;
        public double fieldpublic8;
        public string s;
        public DateTime d;

        public string Str { get; set; }
        public MyTestClass proverty2 { get; set; }

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
        public string fieldpublic;
        public MyTestClass test;
        public Bar bar;

        public string Str { get; set; }

        public Foo()
        {

        }
    }

    public class Bar
    {
        public bool fieldpublic;
        public Foo foo;
        public short fieldpublic2;
        public DateTime d;

        public string Str { get; set; }

        public Bar()
        {

        }
    }
}
