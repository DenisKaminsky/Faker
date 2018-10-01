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
            //faker.Create<MyTestClass>();
            int a = new int();
            Type t = a.GetType();
            Console.WriteLine(a.GetType());
            Console.WriteLine(Marshal.SizeOf(t));
        }
    }

    public class MyTestClass
    {
        private int fieldprivate;
        public int fieldpublic;
        public float fieldpublic2;

        public string Str { get; set; }
        public MyTestClass property1 { get; set; }

        public MyTestClass(int f1,float f2, string f3)
        {
            fieldpublic = f1;
            fieldpublic2 = f2;
            Str = f3;
        }

        public MyTestClass()
        {

        }

    }
}
