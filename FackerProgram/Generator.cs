using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FackerProgram
{
    public class Generator
    {
        private BaseGenerator baseGenerator;
        private DateTimeGenerator dateTimeGenerator;
        private CollectionsGenerator collectionGenerator;
        private Dictionary<Type,Func<object>> typeDictionary;
        private List<Type> dtoTypeList;

        public Generator()
        {
            baseGenerator = new BaseGenerator();
            dateTimeGenerator = new DateTimeGenerator();
            collectionGenerator = new CollectionsGenerator();
            typeDictionary = new Dictionary<Type, Func<object>>();
            dtoTypeList = new List<Type>();
            FillDictionary();
        }

        public void DTOAddType(Type t)
        {
            if (!dtoTypeList.Contains(t))
                dtoTypeList.Add(t);
        }

        public void DTORemoveType(Type t)
        {
            dtoTypeList.Remove(t);
        }

        private void FillDictionary()
        {
            typeDictionary.Add(typeof(Int16), baseGenerator.GenerateShort);
            typeDictionary.Add(typeof(Int32), baseGenerator.GenerateInt);
            typeDictionary.Add(typeof(Int64), baseGenerator.GenerateLong);
            typeDictionary.Add(typeof(UInt16), baseGenerator.GenerateUShort);
            typeDictionary.Add(typeof(UInt32), baseGenerator.GenerateUInt);
            typeDictionary.Add(typeof(UInt64), baseGenerator.GenerateULong);
            typeDictionary.Add(typeof(Double), baseGenerator.GenerateDouble);
            typeDictionary.Add(typeof(Single), baseGenerator.GenerateFloat);
            typeDictionary.Add(typeof(Char), baseGenerator.GenerateChar);
            typeDictionary.Add(typeof(Boolean), baseGenerator.GenerateBool);
            typeDictionary.Add(typeof(Byte), baseGenerator.GenerateByte);
            typeDictionary.Add(typeof(String), baseGenerator.GenerateString);
            typeDictionary.Add(typeof(Object), baseGenerator.GenerateObject);
            typeDictionary.Add(typeof(DateTime), dateTimeGenerator.GenerateDate);
        }

        //генератор значений(общий)
        public object GenerateValue(Type t)
        {
            object obj = null;
            Func<object> generatorDelegate = null;

            if (t.IsArray || t.IsGenericType)
            {
                if (t.IsGenericType)
                    obj = collectionGenerator.GenerateList(t.GenericTypeArguments[0], this);
                else
                    obj = collectionGenerator.GenerateArray(t.GetElementType(), this);
            }
            else if(typeDictionary.TryGetValue(t, out generatorDelegate))
                obj = generatorDelegate.Invoke();
            return obj;
        }
    }
}
