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
        private BaseGenerator baseGenerator;
        private DateTimeGenerator dateTimeGenerator;
        public Faker()
        {
            baseGenerator = new BaseGenerator();
            dateTimeGenerator = new DateTimeGenerator();
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
                    obj = baseGenerator.GenerateShort();
                    break;
                case "System.Int32":
                    obj = baseGenerator.GenerateInt();
                    break;
                case "System.Int64":
                    obj = baseGenerator.GenerateLong();
                    break;
                case "System.UInt16":
                    obj = baseGenerator.GenerateUShort();
                    break;
                case "System.UInt32":
                    obj = baseGenerator.GenerateUInt();
                    break;
                case "System.UInt64":
                    obj = baseGenerator.GenerateULong();
                    break;
                case "System.Double":
                    obj = baseGenerator.GenerateDouble();
                    break;
                case "System.Single":
                    obj = baseGenerator.GenerateFloat();
                    break;
                case "System.Char":
                    obj = baseGenerator.GenerateChar();
                    break;
                case "System.Boolean":
                    obj = baseGenerator.GenerateBool();
                    break;
                case "System.Byte":
                    obj = baseGenerator.GenerateByte();
                    break;
                case "System.String":
                    obj = baseGenerator.GenerateString();
                    break;
                case "System.DateTime":
                    obj = dateTimeGenerator.GenerateDate();
                    break;      
            }
            return obj;
        }

        //создание обьекта инициализацией полей и свойств
        public T CreateByFillingFields<T>()
        {
            Type t = typeof(T);            
            T obj = (T)Activator.CreateInstance(t);   //System.MissingMethodException();

            //инициализация полей
            FieldInfo[] fields = t.GetFields();
            foreach (FieldInfo field in fields)
            {
                field.SetValue(obj, GenerateValue(field.FieldType));
            }
            //инициализация свойств
            PropertyInfo[] properties = t.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.CanWrite && property.SetMethod.IsPublic)
                    property.SetValue(obj, GenerateValue(property.PropertyType));
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
            PropertyInfo[] properties = t.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Console.WriteLine(property.PropertyType + "  " + property.Name);
                bool a = (property.CanWrite  && property.SetMethod.IsPublic);
                int b = 0;
            }

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
