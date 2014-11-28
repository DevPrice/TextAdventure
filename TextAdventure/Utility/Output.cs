using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Utility
{
    public static class Output
    {
        public static void Write(string value)
        {
            Console.Write(value);
        }

        public static void Write(string value, params object[] args)
        {
            Write(String.Format(value, args));
        }

        public static void WriteLine()
        {
            WriteLine(String.Empty);
        }

        public static void WriteLine(string value)
        {
            Write(value + Environment.NewLine);
        }

        public static void WriteLine(string value, params object[] args)
        {
            WriteLine(String.Format(value, args));
        }
    }
}
