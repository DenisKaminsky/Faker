using System;

namespace FackerProgram
{
    public interface IGenerator
    {
        object Generate();
        Type GetValueType();
    }
}
