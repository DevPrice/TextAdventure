using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Commands
{
    public interface ICommandSender
    {
        CommandPermission Permission { get; }

        void SendMessage();
        void SendMessage(string message);
        void SendMessage(string message, params object[] args);
    }
}
