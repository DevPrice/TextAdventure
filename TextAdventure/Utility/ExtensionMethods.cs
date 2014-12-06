using System;
using System.Collections.Generic;
using System.Linq;
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
                if (item.Name.Equals(name))
                    return item;
            }

            return default(T);
        }

        public static T TestTest<T>(this T x)
        {
            return x;
        }
    }
}
