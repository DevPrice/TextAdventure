using Rant;
using Rant.Vocabulary;
using System;

namespace TextAdventure
{
    public static class RantEngine
    {
        private readonly static Rant.RantEngine Engine;

        static RantEngine()
        {
            Engine = new Rant.RantEngine(@"dic", NsfwFilter.Disallow);
        }

        public static string RunPattern(string pattern)
        {
            return Engine.Do(pattern);
        }

        public static string RunPattern(string pattern, params object[] args)
        {
            return RunPattern(String.Format(pattern, args));
        }
    }

    public class RantPatternAttribute : Attribute
    {
        public readonly string Pattern;

        public RantPatternAttribute(string pattern)
        {
            Pattern = pattern;
        }
    }
}
