﻿using System;
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
            MyTestClass c = faker.Create<MyTestClass>();
            faker.PrintObject(c);
        }
    }

    public class MyTestClass
    {
        public string[] mass;
        public List<List<bool>> list;
        //public Foo foo;
        private int fieldprivate;
        public int fieldpublic;
        
        public short fieldpublic2;
        //public char fieldpublic3;
        //public byte fieldpublic4;
        //public long fieldpublic5;
        //public bool fieldpublic6;
        //public float fieldpublic7;
        public double fieldpublic8;
        public string s;
        //public string s2;
        public DateTime d;
        public string Str { get; set; }
        //public MyTestClass property1 { get; set; }
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
}
