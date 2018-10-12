using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace FackerProgram
{
    public sealed class ConsolePrinter:IPrinter
    {
        private List<Type> _DTOList;

        public ConsolePrinter()
        {
            _DTOList = new List<Type>();
        }

        public void DTOListAdd(Type t)
        {
            _DTOList.Add(t);
        }

        public void DTOListRemove(Type t)
        {
            _DTOList.Remove(t);
        }

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
                    PrintArray(obj, indent+"  ");
                else if (obj.GetType().IsGenericType)
                    PrintList(obj, indent);
                else if (_DTOList.Contains(obj.GetType()))
                {
                    Console.WriteLine();
                    Print(obj, indent + "  ");
                }
                else
                    Console.WriteLine(obj);
            }
            else
                Console.WriteLine("null");
        }

        public void Print(object obj,String indent)
        {
            Type t;

            if (obj != null)
            {
                t = obj.GetType();
                Console.WriteLine(indent + "Fields:");
                FieldInfo[] fields = t.GetFields();
                foreach (FieldInfo field in fields)
                {
                    Console.Write(indent + "- " + field.FieldType + "  " + field.Name + "  ");
                    PrintValue(field.GetValue(obj), indent);
                }

                Console.WriteLine(indent + "Propeties:");
                PropertyInfo[] properties = t.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    if (property.CanWrite && property.SetMethod.IsPublic)
                    {
                        Console.Write(indent + "- " + property.PropertyType + "  " + property.Name + "  ");
                        PrintValue(property.GetValue(obj), indent);
                    }
                }
                Console.WriteLine();
            }
            else
                Console.WriteLine("null");
        }
    }
}
