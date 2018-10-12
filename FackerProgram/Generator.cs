using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace FackerProgram
{
    public class Generator
    {
        private BaseGenerator _baseGenerator;
        private DateTimeGenerator _dateTimeGenerator;
        private CollectionsGenerator _collectionGenerator;
        private Dictionary<Type,Func<object>> _typeDictionary;
        private List<Type> _dtoTypeList;
        private List<Type> _cycleList;
        private Faker _faker;
        private Assembly _asm;

        public Generator()
        {            
            _baseGenerator = new BaseGenerator();
            _dateTimeGenerator = new DateTimeGenerator();
            _collectionGenerator = new CollectionsGenerator();
            _typeDictionary = new Dictionary<Type, Func<object>>();
            _dtoTypeList = new List<Type>();
            _cycleList = new List<Type>();
            _asm = Assembly.LoadFile("C:\\Users\\Денис\\Documents\\GitHub\\Faker\\Plugins\\bin\\Debug\\Plugins.dll");
            FillDictionary();
        }

        public void AddToCycle(Type t)
        {
            _cycleList.Add(t);
        }

        public void RemoveFromCycle(Type t)
        {
            _cycleList.Remove(t);
        }

        public void SetFaker(Faker faker)
        {
            _faker = faker;
        }

        public void DTOAddType(Type t)
        {
            if (!_dtoTypeList.Contains(t))
                _dtoTypeList.Add(t);
        }

        public void DTORemoveType(Type t)
        {
            _dtoTypeList.Remove(t);
        }

        private void FillDictionary()
        {
            Type[] types;

            //_typeDictionary.Add(typeof(Int16), _baseGenerator.GenerateShort);
            _typeDictionary.Add(typeof(Int32), _baseGenerator.GenerateInt);
            _typeDictionary.Add(typeof(Int64), _baseGenerator.GenerateLong);
            _typeDictionary.Add(typeof(UInt16), _baseGenerator.GenerateUShort);
            _typeDictionary.Add(typeof(UInt32), _baseGenerator.GenerateUInt);
            _typeDictionary.Add(typeof(UInt64), _baseGenerator.GenerateULong);
            //_typeDictionary.Add(typeof(Double), _baseGenerator.GenerateDouble);
            _typeDictionary.Add(typeof(Single), _baseGenerator.GenerateFloat);
            _typeDictionary.Add(typeof(Char), _baseGenerator.GenerateChar);
            _typeDictionary.Add(typeof(Boolean), _baseGenerator.GenerateBool);
            _typeDictionary.Add(typeof(Byte), _baseGenerator.GenerateByte);
            _typeDictionary.Add(typeof(SByte), _baseGenerator.GenerateSByte);
            _typeDictionary.Add(typeof(String), _baseGenerator.GenerateString);
            _typeDictionary.Add(typeof(Object), _baseGenerator.GenerateObject);
            _typeDictionary.Add(typeof(DateTime), _dateTimeGenerator.GenerateDate);
            
            types = _asm.GetTypes();
            foreach (var type in types)
            {
                if (type.GetInterface(typeof(IGenerator).ToString()) != null)
                {
                    var plugin = _asm.CreateInstance(type.FullName) as IGenerator;
                    if (!_typeDictionary.ContainsKey(plugin.GetValueType()))
                        _typeDictionary.Add(plugin.GetValueType(), plugin.Generate);
                }
            }
        }

        //генератор значений(общий)
        public object GenerateValue(Type t)
        {
            
            object obj = null;
            Func<object> generatorDelegate = null;

            if (t.IsArray || t.IsGenericType)
            {
                if (t.IsGenericType)
                    obj = _collectionGenerator.GenerateList(t.GenericTypeArguments[0], this);
                else
                    obj = _collectionGenerator.GenerateArray(t.GetElementType(), this);
            }
            else if (_typeDictionary.TryGetValue(t, out generatorDelegate))
                obj = generatorDelegate.Invoke();
            else if (_dtoTypeList.Contains(t))
            {
                if (!_cycleList.Contains(t))                
                    obj = _faker.Create(t);                
            }
            return obj;
        }
    }
}
