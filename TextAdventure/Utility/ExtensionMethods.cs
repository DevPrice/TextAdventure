using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TextAdventure.Utility
{
    public static class ExtensionMethods
    {
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }

        public static string GetFullName(this INoun noun)
        {
            return GetFullName(noun, noun.Article);
        }

        public static string GetFullName(this INoun noun, Article article)
        {
            return RantEngine.RunPattern(String.Format("{0}{1}", article == Article.None ? "" : article.GetRantPattern() + " ", noun.Name));
        }

        public static string FirstCharToUpper(this string s)
        {
            if (s.Length == 0)
                return s;

            char[] chars = s.ToCharArray();
            chars[0] = Char.ToUpper(chars[0]);

            return new String(chars);
        }

        public static string ToTitleCase(this string s)
        {
            return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(s);
        }

        /// <summary>
        /// Gets an item from a collection by its name. Returns null if no item exists by the given name.
        /// </summary>
        /// <returns>An item in the collection by the given name, or null if no such item exists.</returns>
        public static T GetByName<T>(this IEnumerable<T> collection, string name) where T : INamed
        {
            foreach (T item in collection)
            {
                if (item.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
                    return item;
            }

            return default(T);
        }

        public static string GetRantPattern<T>(this T enumerationValue)
            where T : struct
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            }

            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(RantPatternAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((RantPatternAttribute)attrs[0]).Pattern;
                }
            }

            return enumerationValue.ToString().ToLower();
        }

        public static string ToSymbol(this Gender gender)
        {
            switch (gender)
            {
                case Gender.Male:
                    return "♂";
                case Gender.Female:
                    return "♀";
                default:
                    return " ";//"⚲";
            }
        }
    }
}
