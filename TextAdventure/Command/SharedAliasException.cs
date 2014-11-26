using System;

namespace TextAdventure.Command
{
    public class SharedAliasException : Exception
    {
        public string Alias { get; private set; }

        public SharedAliasException(string alias)
            : base("The specified alias references more than one command.")
        {
            Alias = alias;
        }
    }
}
