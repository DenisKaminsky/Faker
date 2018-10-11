using System;

namespace FackerProgram
{
    public class DateTimeGenerator
    {
        private Random _rand;

        public DateTimeGenerator()
        {
            _rand = new Random(); 
        }
        
        //генератор даты
        public object GenerateDate()
        {
            DateTime d = new DateTime(_rand.Next(1, 10000), _rand.Next(1, 13), _rand.Next(1, 29), _rand.Next(0, 24), _rand.Next(0, 60), _rand.Next(0, 60));
            return d;
        }
    }
}
