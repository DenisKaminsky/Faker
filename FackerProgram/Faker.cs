using System;
using System.Linq;
using System.Reflection;

namespace FackerProgram
{
    public sealed class Faker : IFacker
    {
        private Generator generator;

        public Faker()
        {
            generator = new Generator();
        }

        public void DTOAdd(Type t)
        {
            generator.DTOAddType(t);
        }

        public void DTORemove(Type t)
        {
            generator.DTORemoveType(t);
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

        //создание обьекта инициализацией полей и свойств
        public T CreateByFillingFields<T>()
        {
            Type t = typeof(T);            
            T obj = (T)Activator.CreateInstance(t);   //System.MissingMethodException();

            //инициализация полей
            FieldInfo[] fields = t.GetFields();
            foreach (FieldInfo field in fields)
            {
                try
                {
                    field.SetValue(obj, generator.GenerateValue(field.FieldType));
                }
                catch(FieldAccessException e)
                {
                    //...const
                }
            }
            //инициализация свойств
            PropertyInfo[] properties = t.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.CanWrite && property.SetMethod.IsPublic)
                    property.SetValue(obj, generator.GenerateValue(property.PropertyType));
            }
            return obj;
        }

        public T CreateByConstructor<T>(ConstructorInfo constructor)
        {
            Type t = typeof(T);
            object[] parametersValues = new object[constructor.GetParameters().Count<ParameterInfo>()];
            ParameterInfo[] parameters =  constructor.GetParameters();
            int i = 0;
            foreach (ParameterInfo parameter in parameters)
            {
                parametersValues[i] = generator.GenerateValue(parameter.ParameterType);
                i++;
            }
            T obj = (T)constructor.Invoke(parametersValues);
            return obj;
        }        

        public T Create<T>()
        {
            Type t = typeof(T);
            ConstructorInfo constructor = FindMaxParamsConstructor(t);
            int constructorParametersCount = constructor.GetParameters().Count<ParameterInfo>();
            int publicFieldCount = t.GetFields().Count<FieldInfo>();
            int publicPropertiesCount = t.GetProperties().Count<PropertyInfo>();

            if (constructorParametersCount >= publicFieldCount + publicPropertiesCount)
            {
                Console.WriteLine("Object was create by constructor\n");
                return CreateByConstructor<T>(constructor);
            }
            Console.WriteLine("Object was create by filling fields\n");
            return CreateByFillingFields<T>();
        }
    }
}
