using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Commands
{
    public interface ICommandSender
    {
        string Name { get; }
        CommandPermission Permission { get; }
    }
}
