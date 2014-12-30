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

        void Send();
        void Send(string message);
        void Send(string message, params object[] args);
        void SendLine();
        void SendLine(string message);
        void SendLine(string message, params object[] args);
    }
}
