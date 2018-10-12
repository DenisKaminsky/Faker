using System;
using FackerProgram;

namespace Plugins
{
    public class ShortGenerator:IGenerator
    {
        public object Generate()
        {
            Random rand = new Random();
            byte[] bytes = new byte[sizeof(short)];

            rand.NextBytes(bytes);
            return BitConverter.ToInt16(bytes, 0);
        }

        public Type GetValueType()
        {
            return typeof(short);
        }
    }
}
