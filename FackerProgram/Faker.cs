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
        public Faker()
        {

        }

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
        
        private object GenerateObject(ConstructorInfo constructor)
        {
            object obj = new object();
            ParameterInfo[] parameters = constructor.GetParameters();
            foreach (ParameterInfo parameter in parameters)
            {
                //parameter.
            }
            return obj;
        }

        public T CreateByFillingField<T>()
        {
            T obj;
            Type t = typeof(T);
            ConstructorInfo constructor = FindMinParamsConstructor(t);
            obj = (T)GenerateObject(constructor);
            return obj;
        }

        public T CreateByConstructor<T>()
        {
            T obj;
            Type t = typeof(T);
            ConstructorInfo constructor = FindMaxParamsConstructor(t);
            obj = (T)GenerateObject(constructor);
            return obj;
        }

        public T Create<T>()
        {
            Type t = typeof(T);
            FieldInfo[] fields = t.GetFields();
            Console.WriteLine("Fields:");
            foreach (FieldInfo field in fields)
                Console.WriteLine(field.FieldType + "  " + field.Name);

            Console.WriteLine("\nPropeties: ");
            PropertyInfo[] properties = t.GetProperties();
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
