using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.World;

namespace TextAdventure.Command
{
    public class CommandQuit : CommandBase
    {
        public CommandQuit()
        {
            Name = "quit";
            Aliases.Add("exit");
        }

        public override void Execute(ICommandSender sender)
        {
            Environment.Exit(Environment.ExitCode);
        }

        public override ICommand Create(string[] args)
        {
            return new CommandQuit();
        }
    }
}
