using System;
using System.Runtime.Serialization;

namespace TextAdventure.Commands
{
    [Serializable]
    public class SharedAliasException : Exception
    {
        public string Alias { get; private set; }

        public SharedAliasException(string alias)
            : base("The specified alias references more than one command.")
        {
            Alias = alias;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
