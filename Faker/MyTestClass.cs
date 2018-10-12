using System;
using System.Collections.Generic;

namespace Faker
{
    public class MyTestClass
    {
        private int _fieldPrivateInt;
        public bool[] _mass;
        public List<int> _list;
        public Foo _foo;
        public Bar _bar;
        public int _fieldInt;
        public short _fieldShort;
        public long _fieldLong;
        public ushort _fieldUShort;
        public uint _fieldUInt;
        public ulong _fieldULong;
        public double _fieldDouble;
        public float _fieldFloat;
        public byte _fieldByte;
        public sbyte _fieldSByte;
        public string _fieldString;
        public bool _fieldBool;
        public DateTime _fieldDate;
        public object _fieldObject;

        public string propertyString { get; set; }
        public MyTestClass propertyTest { get; set; }
        public char propertyChar { get; set; }

        public MyTestClass(int value1, short value2, string value3)
        {
            _fieldPrivateInt = value1;
            _fieldShort = value2;
            _fieldString = value3;
        }

        public MyTestClass()
        {

        }
    }
}
