using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace FackerProgram
{
    public sealed class Faker : IFacker
    {
        private Generator _generator;

        public Faker()
        {
            _generator = new Generator();
        }

        public void DTOAdd(Type t)
        {
            _generator.DTOAddType(t);
        }

        public void DTORemove(Type t)
        {
            _generator.DTORemoveType(t);
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
        public object CreateByFillingFields(Type t)
        {          
            object obj = Activator.CreateInstance(t);   //System.MissingMethodException();

            //инициализация полей
            FieldInfo[] fields = t.GetFields();
            foreach (FieldInfo field in fields)
            {
                try
                {
                    field.SetValue(obj, _generator.GenerateValue(field.FieldType));
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
                    property.SetValue(obj, _generator.GenerateValue(property.PropertyType));
            }
            return obj;
        }

        public object CreateByConstructor(ConstructorInfo constructor,Type t)
        {
            object[] parametersValues = new object[constructor.GetParameters().Count<ParameterInfo>()];
            ParameterInfo[] parameters =  constructor.GetParameters();
            int i = 0;
            foreach (ParameterInfo parameter in parameters)
            {
                parametersValues[i] = _generator.GenerateValue(parameter.ParameterType);
                i++;
            }
            return constructor.Invoke(parametersValues);
        }        

        public object Create(Type t)
        {
            object result;
            _generator.AddToCycle(t);
            ConstructorInfo constructor = FindMaxParamsConstructor(t);
            int constructorParametersCount = constructor.GetParameters().Count<ParameterInfo>();
            int publicFieldCount = t.GetFields().Count<FieldInfo>();
            int publicPropertiesCount = t.GetProperties().Count<PropertyInfo>();

            if (constructorParametersCount >= publicFieldCount + publicPropertiesCount)
            {
                Console.WriteLine("Object was create by constructor\n");
                result = CreateByConstructor(constructor,t);
            }
            //Console.WriteLine("Object was create by filling fields\n");
            result = CreateByFillingFields(t);
            _generator.RemoveFromCycle(t);
            return result;
        }

        public T Create<T>()
        {
            _generator.SetFaker(this);
            Type t = typeof(T);
            return (T)Create(t);
        }
    }
}
