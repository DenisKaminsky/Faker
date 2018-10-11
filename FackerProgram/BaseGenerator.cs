using System;
using System.Reflection;

namespace FackerProgram
{
    public class BaseGenerator
    {
        private Random rand;

        public BaseGenerator()
        {
            rand = new Random();            
        }

        //генератор Short(INT16)
        public object GenerateShort() 
        {
            byte[] bytes = new byte[sizeof(short)]; 
            rand.NextBytes(bytes);
            return BitConverter.ToInt16(bytes, 0);
        }

        //генератор Int(INT32)
        public object GenerateInt()
        {
            byte[] bytes = new byte[sizeof(int)];
            rand.NextBytes(bytes);
            return BitConverter.ToInt32(bytes,0);
        }

        //генератор Long(INT64)
        public object GenerateLong()
        {
            byte[] bytes = new byte[sizeof(long)];
            rand.NextBytes(bytes);
            return BitConverter.ToInt64(bytes,0);
        }

        //генератор UShort(UINT16)
        public object GenerateUShort()
        {
            byte[] bytes = new byte[sizeof(ushort)];
            rand.NextBytes(bytes);
            return BitConverter.ToUInt16(bytes, 0);
        }

        //генератор UInt(UINT32)
        public object GenerateUInt()
        {
            byte[] bytes = new byte[sizeof(uint)];
            rand.NextBytes(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }

        //генератор ULONG(UINT64)
        public object GenerateULong()
        {
            byte[] bytes = new byte[sizeof(ulong)];
            rand.NextBytes(bytes);
            return BitConverter.ToUInt64(bytes, 0);
        }

        //генератор Bool
        public object GenerateBool()
        {
            return Convert.ToBoolean(rand.Next(0, 2));
        }

        //генератор Byte
        public object GenerateByte()
        {            
            return Convert.ToByte(rand.Next(0, 256));
        }

        //генератор Char
        public object GenerateChar()
        {
            byte[] bytes = new byte[sizeof(char)];
            rand.NextBytes(bytes);
            return BitConverter.ToChar(bytes, 0);
        }

        //генератор Double
        public object GenerateDouble()
        {
            byte[] bytes = new byte[sizeof(double)];
            rand.NextBytes(bytes);
            return BitConverter.ToDouble(bytes, 0);
        }

        //генератор float
        public object GenerateFloat()
        {
            byte[] bytes = new byte[sizeof(float)];
            rand.NextBytes(bytes);
            return BitConverter.ToSingle(bytes, 0);
        }

        //генератор string
        public object GenerateString()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            int length = rand.Next(1, 21);
            string s ="";
            for (int i = 0; i < length; i++)
            {
                //s += Convert.ToChar(GenerateByte());
                //s+=GenerateChar();
                s += chars[rand.Next(0, chars.Length)];
            }
            return s;
        }

        //генератор object(Должен быть последним!!!)
        public object GenerateObject()
        {
            MethodInfo[] methods = this.GetType().GetMethods();
            object obj = methods[rand.Next(0, methods.Length - 4)].Invoke(this, new object[] { });
            return obj;
        }
    }
}
