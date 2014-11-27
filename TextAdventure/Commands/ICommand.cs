using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.World;

namespace TextAdventure.Commands
{
    public interface ICommand
    {
        string Name { get; }
        List<string> Aliases { get; }
        CommandPermission RequiredPermission { get; }
        bool Hidden { get; }

        void Execute(ICommandSender sender);
        ICommand Create(string[] args);
    }
}
