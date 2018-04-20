using System;

namespace Core
{
    public class FuncEnumPair<T, TEnum> where TEnum : struct
    {
        public Func<T, T, bool> Func { get; }
        public TEnum Value { get; }

        public FuncEnumPair(Func<T, T, bool> func, TEnum enumVal)
        {
            Func = func;
            Value = enumVal;
        }
    }
}
