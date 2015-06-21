using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Utility
{
    public static class Output
    {
        public static void Write(object value)
        {
            Console.Write(value);
        }

        public static void Write(object value, params object[] args)
        {
            value = value ?? String.Empty;
            Write(String.Format(value.ToString(), args));
        }

        public static void WriteLine()
        {
            WriteLine(String.Empty);
        }

        public static void WriteLine(object value)
        {
            Write(value + Environment.NewLine);
        }

        public static void WriteLine(object value, params object[] args)
        {
            value = value ?? String.Empty;
            WriteLine(String.Format(value.ToString(), args));
        }
    }
}
