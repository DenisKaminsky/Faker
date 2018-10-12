using System;

namespace Faker
{
    public class Bar
    {
        private string _fieldString;
        public bool _fieldBool;
        public Foo _fieldFoo;
        public DateTime _fieldDate;

        public string propertyString
        {
            get { return _fieldString; }
            set { _fieldString = value; }
        }

        public Bar(bool fieldBool, Foo filedFoo, DateTime fieldDate, String fieldString)
        {
            _fieldBool = fieldBool;
            _fieldFoo = filedFoo;
            _fieldDate = fieldDate;
            propertyString = fieldString;
        }
    }
}
