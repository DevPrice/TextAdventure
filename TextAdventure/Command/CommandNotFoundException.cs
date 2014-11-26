using System;

namespace TextAdventure.Command
{
    public class CommandNotFoundException : Exception
    {
        public string Name { get; private set; }

        public CommandNotFoundException(string name)
            : base("Command not found.")
        {
            Name = name;
        }
    }
}
