using System;
using FackerProgram;

namespace Plugins
{
    public class DoubleGenerator:IGenerator
    {
        public object Generate()
        {
            Random rand = new Random();
            byte[] bytes = new byte[sizeof(double)];

            rand.NextBytes(bytes);
            return BitConverter.ToDouble(bytes, 0);
        }

        public Type GetValueType()
        {
            return typeof(Double);
        }
    }
}
