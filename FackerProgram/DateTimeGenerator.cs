using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FackerProgram
{
    public class DateTimeGenerator
    {
        private Random rand;

        public DateTimeGenerator()
        {
            rand = new Random(); 
        }
        
        //генератор даты
        public DateTime GenerateDate()
        {
            DateTime d = new DateTime(rand.Next(1, 10000), rand.Next(1, 13), rand.Next(1, 29), rand.Next(0, 24), rand.Next(0, 60), rand.Next(0, 60));
            return d;
        }
    }
}
