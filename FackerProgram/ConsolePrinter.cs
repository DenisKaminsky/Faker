using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FackerProgram
{
    public sealed class ConsolePrinter:IPrinter
    {
        private void PrintArray(object obj, string indent)
        {
            Array array = (Array)obj;
            object value;

            Console.WriteLine("Length = " + array.Length);
            for (int i = 0; i < array.Length; i++)
            {
                value = array.GetValue(i);
                Console.Write(indent + "[" + i + "] ");
                if (value.GetType().IsArray || value.GetType().IsGenericType)
                {
                    PrintValue(value, indent + "    ");
                }
                else
                    Console.WriteLine(value);
            }
        }

        private void PrintList(object obj,string indent)
        {
            object value;
            IList list = (IList)obj;

            Console.WriteLine("Length = " + list.Count);
            for (int i = 0; i < list.Count; i++)
            {
                value = list[i];
                Console.Write(indent + "<" + i + "> ");
                if (value.GetType().IsArray || value.GetType().IsGenericType)
                {
                    PrintValue(value, indent + "    ");
                }
                else
                    Console.WriteLine(value);
            }
        }

        private void PrintValue(object obj,string indent)
        {
            if (obj != null)
            {
                if (obj.GetType().IsArray)
                    PrintArray(obj, indent);
                else if (obj.GetType().IsGenericType)
                    PrintList(obj, indent);
                else
                    Console.WriteLine(obj);
            }
            else
                Console.WriteLine("null");
        }

        public void Print(object obj)
        {
            Type t = obj.GetType();
            Console.WriteLine("Fields:\n");
            FieldInfo[] fields = t.GetFields();
            foreach (FieldInfo field in fields)
            {
                Console.Write(field.FieldType + "  " + field.Name + "  ");
                PrintValue(field.GetValue(obj),"");
            }

            Console.WriteLine("\nPropeties:\n");
            PropertyInfo[] properties = t.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.CanWrite && property.SetMethod.IsPublic)
                {
                    Console.Write(property.PropertyType + "  " + property.Name+ "  ");
                    PrintValue(property.GetValue(obj), "");
                }
            }
            Console.WriteLine();
        }
    }
}
