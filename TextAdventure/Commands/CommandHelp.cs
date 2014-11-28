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
        public CommandHelp(GameWorld world, ICommandSender sender, IEnumerable<ICommand> commands)
            : base(world, sender, commands)
        {
            Name = "help";
            Aliases.Add("?");
        }

        public override void Execute()
        {
            base.Execute();


            ListCommands(Sender);
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

        public override ICommand Create(ICommandSender sender, string[] args)
        {
            return new CommandHelp(World, sender, Target);
        }
    }
}
