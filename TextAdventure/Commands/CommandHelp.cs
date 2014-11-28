using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Utility;
using TextAdventure.World;

namespace TextAdventure.Commands
{
    class CommandHelp : Command<IEnumerable<ICommand>>
    {
        public CommandHelp(GameWorld world, IEnumerable<ICommand> commands)
            : base(world, commands)
        {
            Name = "help";
            Aliases.Add("?");
        }

        public override void Execute(ICommandSender sender)
        {
            base.Execute(sender);


            ListCommands(sender);
        }

        private void ListCommands(ICommandSender sender)
        {
            Output.WriteLine("Available commands:");

            foreach (ICommand command in Target)
            {
                if (command.Hidden || (int)sender.Permission < (int)command.RequiredPermission)
                    continue;

                Output.WriteLine("{0}", command.Name);
            }

            Output.WriteLine();
            Output.WriteLine("Type 'help [command]' for more information about a command.");
        }

        public override ICommand Create(string[] args)
        {
            return new CommandHelp(World, Target);
        }
    }
}
