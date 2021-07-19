using System.Collections.Generic;

namespace CommandPattern.Utils
{
    public static class GenericExtensions
    {
        public static bool IsNullOrEmpty<T>(this IList<T> list)
            where T : class => (list == null) || (list.Count == 0);

        public static int GetGenderFromCharToInt(this char gender)
        {
            switch (gender.ToString().ToUpperInvariant())
            {
                case "M":
                    return 0;
                case "V":
                    return 1;
                default:
                    return 2;
            }
        }

        public static char GetGenderFromIntToChar(this int gender)
        {
            switch (gender)
            {
                case 0:
                    return 'M';
                case 1:
                    return 'V';
                default:
                    return 'A'; // from other gender type
            }
        }

        public static int? GetGenderFromCharToInt(this char? gender)
        {
            if (!gender.HasValue
                || (gender == ' ')
                || (gender == '\x0000'))
            {
                return null;
            }

            return gender.Value.GetGenderFromCharToInt();
        }

        public static char? GetGenderFromIntToChar(this int? gender) => gender?.GetGenderFromIntToChar();

        public static char MapBoolToCharValue(this bool boolValue) => boolValue ? 'T' : 'F';
        public static bool MapCharValueToBool(this char charValue) => charValue.ToString().ToUpper().Equals("T");
    }
}