using System;

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
        public short GenerateShort()
        {
            byte[] bytes = new byte[2];
            rand.NextBytes(bytes);
            return BitConverter.ToInt16(bytes, 0);
        }

        //генератор Int(INT32)
        public int GenerateInt()
        {
            byte[] bytes = new byte[4];
            rand.NextBytes(bytes);
            return BitConverter.ToInt32(bytes,0);
        }

        //генератор Long(INT64)
        public long GenerateLong()
        {
            byte[] bytes = new byte[8];
            rand.NextBytes(bytes);
            return BitConverter.ToInt64(bytes,0);
        }

        //генератор UShort(UINT16)
        public ushort GenerateUShort()
        {
            byte[] bytes = new byte[2];
            rand.NextBytes(bytes);
            return BitConverter.ToUInt16(bytes, 0);
        }

        //генератор UInt(UINT32)
        public uint GenerateUInt()
        {
            byte[] bytes = new byte[4];
            rand.NextBytes(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }

        //генератор ULONG(UINT64)
        public ulong GenerateULong()
        {
            byte[] bytes = new byte[8];
            rand.NextBytes(bytes);
            return BitConverter.ToUInt64(bytes, 0);
        }

        //генератор Bool
        public bool GenerateBool()
        {
            return Convert.ToBoolean(rand.Next(0, 2));
        }

        //генератор Byte
        public byte GenerateByte()
        {            
            return Convert.ToByte(rand.Next(0, 256));
        }

        //генератор Char
        public char GenerateChar()
        {
            byte[] bytes = new byte[2];
            rand.NextBytes(bytes);
            return BitConverter.ToChar(bytes, 0);
        }

        //генератор Double
        public double GenerateDouble()
        {
            byte[] bytes = new byte[8];
            rand.NextBytes(bytes);
            return BitConverter.ToDouble(bytes, 0);
        }

        //генератор float
        public float GenerateFloat()
        {
            byte[] bytes = new byte[4];
            rand.NextBytes(bytes);
            return BitConverter.ToSingle(bytes, 0);
        }

        public string GenerateString()
        {
            int length = rand.Next(1, 100);
            string s ="";
            for (int i = 0; i < length; i++)
            {
                //s += ();
            }
            return s;
        }
    }
}
