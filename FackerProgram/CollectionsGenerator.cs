using System;
using System.Collections;
using System.Collections.Generic;

namespace FackerProgram
{
    public class CollectionsGenerator
    {
        private Random _rand;
        private BaseGenerator _baseGenerator;

        public CollectionsGenerator()
        {
            _rand = new Random();
            _baseGenerator = new BaseGenerator();
        }

        public object GenerateList(Type t,Generator generator)
        {
            object list = Activator.CreateInstance(typeof(List<>).MakeGenericType(t));
            int count = _rand.Next(1, 21);

            for (int i = 0; i < count; i++)
            {
                ((IList)list).Add(generator.GenerateValue(t));
            }
            return list;
        }

        public object GenerateArray(Type t, Generator generator)
        {
            int count = _rand.Next(1, 21);
            Array mass = Array.CreateInstance(t, count);

            for (int i = 0; i < count; i++)
            {
                mass.SetValue(generator.GenerateValue(t), i);
            }            
            return mass;        
        }

    }
}
