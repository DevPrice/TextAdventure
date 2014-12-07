using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Commands
{
    [Serializable]
    public class InsufficientPermissionException : Exception
    {
        public ICommandSender CommandIssuer { get; private set; }

        public InsufficientPermissionException(ICommandSender issuer)
            : base("Insufficient permission to execute command.")
        {
            CommandIssuer = issuer;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
