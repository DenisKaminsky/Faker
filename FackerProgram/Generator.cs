using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FackerProgram
{
    public class Generator
    {
        private BaseGenerator baseGenerator;
        private DateTimeGenerator dateTimeGenerator;
        private CollectionsGenerator collectionGenerator;

        public Generator()
        {
            baseGenerator = new BaseGenerator();
            dateTimeGenerator = new DateTimeGenerator();
            collectionGenerator = new CollectionsGenerator();
        }

        private object GenerateBaseTypeValue(Type t)
        {
            object obj = null;
            switch (t.ToString())
            {
                case "System.Int16":
                    obj = baseGenerator.GenerateShort();
                    break;
                case "System.Int32":
                    obj = baseGenerator.GenerateInt();
                    break;
                case "System.Int64":
                    obj = baseGenerator.GenerateLong();
                    break;
                case "System.UInt16":
                    obj = baseGenerator.GenerateUShort();
                    break;
                case "System.UInt32":
                    obj = baseGenerator.GenerateUInt();
                    break;
                case "System.UInt64":
                    obj = baseGenerator.GenerateULong();
                    break;
                case "System.Double":
                    obj = baseGenerator.GenerateDouble();
                    break;
                case "System.Single":
                    obj = baseGenerator.GenerateFloat();
                    break;
                case "System.Char":
                    obj = baseGenerator.GenerateChar();
                    break;
                case "System.Boolean":
                    obj = baseGenerator.GenerateBool();
                    break;
                case "System.Byte":
                    obj = baseGenerator.GenerateByte();
                    break;
                case "System.String":
                    obj = baseGenerator.GenerateString();
                    break;
                case "System.Object":
                    obj = baseGenerator.GenerateObject();
                    break;
                case "System.DateTime":
                    obj = dateTimeGenerator.GenerateDate();
                    break;
            }
            return obj;
        }

        //генератор значений(общий)
        public object GenerateValue(Type t)
        {
            object obj = null;
            if (t.IsArray || t.IsGenericType)
            {
                if (t.IsGenericType)
                    obj = collectionGenerator.GenerateList(t.GenericTypeArguments[0], this);
                else
                    obj = collectionGenerator.GenerateArray(t.GetElementType(), this);
            }
            else
                obj = GenerateBaseTypeValue(t);
            return obj;
        }
    }
}
