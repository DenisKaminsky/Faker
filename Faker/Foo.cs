namespace Faker
{
    public class Foo
    {
        public string _fieldString;
        public MyTestClass _fieldTest;
        public Bar _fieldBar;

        public string str { get; set; }

        public Foo(string str)
        {
            _fieldString = str;
        }

        public Foo()
        {

        }
    }
}
