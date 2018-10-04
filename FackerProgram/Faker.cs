using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace FackerProgram
{
    public sealed class Faker
    {
        private BaseGenerator generator;

        public Faker()
        {
            generator = new BaseGenerator();
        }

        //поиск конструктора с минимальным количеством параметров
        private ConstructorInfo FindMinParamsConstructor(Type t)
        {
            ConstructorInfo[] constructors = t.GetConstructors();
            ConstructorInfo minParamConstructor = constructors[0];
            int min = constructors[0].GetParameters().Count<ParameterInfo>();

            foreach (ConstructorInfo constructor in constructors)
            {
                int paramsCount = constructor.GetParameters().Count<ParameterInfo>();
                if (paramsCount < min)
                {
                    min = paramsCount;
                    minParamConstructor = constructor;
                }                    
            }
            return minParamConstructor;                
        }

        //поиск конструктора с макс количеством параметров
        private ConstructorInfo FindMaxParamsConstructor(Type t)
        {
            ConstructorInfo[] constructors = t.GetConstructors();
            ConstructorInfo maxParamConstructor = constructors[0];
            int max = 0;

            foreach (ConstructorInfo constructor in constructors)
            {
                int paramsCount = constructor.GetParameters().Count<ParameterInfo>();
                if (paramsCount > max)
                {
                    max = paramsCount;
                    maxParamConstructor = constructor;
                }
            }
            return maxParamConstructor;
        }       

        //генератор значений(общий)
        private object GenerateValue(Type t)
        {
            object obj = null;

            switch (t.ToString())
            {
                case "System.Int16":
                    obj = generator.GenerateShort();
                    break;
                case "System.Int32":
                    obj = generator.GenerateInt();
                    break;
                case "System.Int64":
                    obj = generator.GenerateLong();
                    break;
                case "System.UInt16":
                    obj = generator.GenerateUShort();
                    break;
                case "System.UInt32":
                    obj = generator.GenerateUInt();
                    break;
                case "System.UInt64":
                    obj = generator.GenerateULong();
                    break;
                case "System.Double":
                    obj = generator.GenerateDouble();
                    break;
                case "System.Single":
                    obj = generator.GenerateFloat();
                    break;
                case "System.Char":
                    obj = generator.GenerateChar();
                    break;
                case "System.Boolean":
                    obj = generator.GenerateBool();
                    break;
                case "System.Byte":
                    obj = generator.GenerateByte();
                    break;
                case "System.String":
                    obj = generator.GenerateString();
                    break;           
            }
            return obj;
        }

        //создание обьекта инициализацией полей
        public T CreateByFillingFields<T>()
        {
            Type t = typeof(T);
            
            T obj = (T)Activator.CreateInstance(t);   //System.MissingMethodException();

            FieldInfo[] fields = t.GetFields();
            foreach (FieldInfo field in fields)
            {
                string s = field.FieldType.ToString();
                field.SetValue(obj, GenerateValue(field.FieldType));
            }
            return obj;
        }

        public T CreateByConstructor<T>()
        {
            Type t = typeof(T);
            ConstructorInfo constructor = FindMaxParamsConstructor(t);
            object[] parametersValues = new object[constructor.GetParameters().Count<ParameterInfo>()];
            ParameterInfo[] parameters =  constructor.GetParameters();
            int i = 0;
            foreach (ParameterInfo parameter in parameters)
            {
                parametersValues[i] = GenerateValue(parameter.ParameterType);
                i++;
            }
            T obj = (T)constructor.Invoke(parametersValues);
            return obj;
        }

        public void Create<T>()
        {
            Type t = typeof(T);
            FieldInfo[] fields = t.GetFields(BindingFlags.Public| BindingFlags.Instance);
            Console.WriteLine("Fields:");
            foreach (FieldInfo field in fields)
                Console.WriteLine(field.FieldType + "  " + field.Name);

            Console.WriteLine("\nPropeties: ");
            PropertyInfo[] properties = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo property in properties)
                Console.WriteLine(property.PropertyType + "  " + property.Name);

            Console.WriteLine("\nMethods: ");
            MethodInfo[] methods = t.GetMethods();
            foreach (MethodInfo method in methods)
                Console.WriteLine(method.ReturnType + "  " + method.Name);

            Console.WriteLine("\nConstructors: ");
            ConstructorInfo[] constructors = t.GetConstructors();
            foreach (ConstructorInfo constructor in constructors)
                Console.WriteLine("Params count: " + constructor.GetParameters().Count<ParameterInfo>() + " " + constructor.Name);
            
        }
    }
}
