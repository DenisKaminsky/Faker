using System;
using System.Linq;
using System.Reflection;

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
            ConstructorInfo minParamConstructor = null;
            int paramsCount;

            foreach (ConstructorInfo constructor in constructors)
            {
                paramsCount = constructor.GetParameters().Count<ParameterInfo>();
                if (paramsCount == 0)
                {
                    minParamConstructor = constructor;
                    break;
                }                    
            }
            return minParamConstructor;                
        }

        //поиск конструктора с макс количеством параметров
        private ConstructorInfo FindMaxParamsConstructor(Type t)
        {
            ConstructorInfo[] constructors = t.GetConstructors();
            ConstructorInfo maxParamConstructor = null;
            int max = 0, paramsCount = 0 ;

            foreach (ConstructorInfo constructor in constructors)
            {
                paramsCount = constructor.GetParameters().Count<ParameterInfo>();
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
            object obj = Activator.CreateInstance(t); 
            FieldInfo[] fields = t.GetFields();
            PropertyInfo[] properties = t.GetProperties();

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

            foreach (PropertyInfo property in properties)
            {
                if (property.CanWrite && property.SetMethod.IsPublic)
                {
                    property.SetValue(obj, _generator.GenerateValue(property.PropertyType));
                }
            }
            return obj;
        }

        public object CreateByConstructor(ConstructorInfo constructor,Type t)
        {
            object[] parametersValues = new object[constructor.GetParameters().Count<ParameterInfo>()];
            ParameterInfo[] parameters =  constructor.GetParameters();
            object result; 

            int i = 0;

            foreach (ParameterInfo parameter in parameters)
            {
                parametersValues[i] = _generator.GenerateValue(parameter.ParameterType);
                i++;
            }
            try
            {
                result = constructor.Invoke(parametersValues);
            }
            catch(OutOfMemoryException e)
            {
                result = null;
            }
            catch(OverflowException e)
            {
                result = null;
            }
            return result;
        }        

        public object Create(Type t)
        {
            object result;
            ConstructorInfo parameterizedConstructor;
            ConstructorInfo nonParameterizedConstructor;
            int publicFieldCount, publicPropertiesCount;

            _generator.AddToCycle(t);
            parameterizedConstructor = FindMaxParamsConstructor(t);
            nonParameterizedConstructor = FindMinParamsConstructor(t);
            publicFieldCount = t.GetFields().Count<FieldInfo>();
            publicPropertiesCount = t.GetProperties().Count<PropertyInfo>();
            if ((parameterizedConstructor == null) || ((nonParameterizedConstructor != null)
                && (parameterizedConstructor.GetParameters().Count<ParameterInfo>() < publicFieldCount + publicPropertiesCount)))
            {
                result = CreateByFillingFields(t);
                Console.WriteLine(result.GetType()+" was create by filling fields");
            }
            else
            {
                result = CreateByConstructor(parameterizedConstructor, t);
                Console.WriteLine(result.GetType()+ " was create by ctor");
            }
            _generator.RemoveFromCycle(t);
            return result;
        }

        public T Create<T>()
        {
            Type t = typeof(T);

            _generator.SetFaker(this);
            return (T)Create(t);
        }
    }
}
