using System;

namespace Core
{
    public static class Utils
    {
        public static bool IsUpperCase(this char value)
        {
            if(!value.IsEnglishLetter())
            {
                throw new ArgumentException("value must be an english letter");
            }

            return (value >= 65 && value <= 90);
        }

        public static bool IsEnglishLetter(this char value)
        {
            return ((value >= 65 && value <= 90) || (value >= 97 && value <= 122));
        }

        public static char ToLower(this char value)
        {
            if (!value.IsEnglishLetter())
            {
                throw new ArgumentException("value must be an english letter");
            }

            return value.IsUpperCase() ? (char)(value + 32) : value;
        }

        public static T TryParseToEnum<T>(this int value) where T : struct
        {
            T enumValue = (T)(object)value;
            if (!Enum.IsDefined(typeof(T), enumValue))
            {
                throw new ArgumentException("value is not defined in enum");
            }

            return enumValue;
        }

        public static T At<T>(this T[][] array, Point pos)
        {
            return array[pos.Y][pos.X];
        }
    }
}
