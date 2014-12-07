using System;
using System.Runtime.Serialization;

namespace TextAdventure.Commands
{
    [Serializable]
    public class CommandNotFoundException : Exception
    {
        public string Name { get; private set; }

        public CommandNotFoundException(string name)
            : base("Command not found.")
        {
            Name = name;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
