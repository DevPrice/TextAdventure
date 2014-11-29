﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Utility;
using TextAdventure.World;

namespace TextAdventure.Commands
{
    public class CommandHelp : Command<ICommand>
    {
        public IEnumerable<ICommand> CommandList { get; private set; }
        public bool UsedAlias { get; private set; }

        public CommandHelp(GameWorld world, ICommandSender sender, IEnumerable<ICommand> commandList, ICommand command)
            : base(world, sender, command)
        {
            Name = "help";
            Aliases.Add("?");
            CommandList = commandList;
        }

        public override void Execute()
        {
            base.Execute();


            ListCommands(Sender);
        }

        private void ListCommands(ICommandSender sender)
        {
            if (Target != null)
            {
                if (Target.Name.Equals("help"))
                {
                    Sender.SendMessage("Are you okay?");
                    return;
                }

                if (UsedAlias)
                    Sender.SendMessage("Alias for {0}.", Target.Name);

                Sender.SendMessage(Target.Description);
                Sender.SendMessage();

                Output.Write("Aliases: ");

                foreach (string alias in Target.Aliases)
                    Output.Write("{0} ", alias);

                Sender.SendMessage();

                Sender.SendMessage("Usage: {0}", Target.Usage);

                return;
            }

            Sender.SendMessage("Available commands:");

            foreach (ICommand command in CommandList)
            {
                if (command.Hidden || (int)sender.Permission < (int)command.RequiredPermission)
                    continue;

                Sender.SendMessage("{0}", command.Name);
            }

            Sender.SendMessage();
            Sender.SendMessage("Type 'help [command]' for more information about a command.");
        }

        public override ICommand Create(ICommandSender sender, string[] args)
        {
            if (args.Length > 1)
            {
                foreach (ICommand command in CommandList)
                {
                    if (command.Name.Equals(args[1], StringComparison.CurrentCultureIgnoreCase))
                        return new CommandHelp(World, sender, CommandList, command);

                    foreach (string alias in command.Aliases)
                    {
                        if (alias.Equals(args[1], StringComparison.CurrentCultureIgnoreCase))
                        {
                            CommandHelp commandHelp = new CommandHelp(World, sender, CommandList, command);
                            commandHelp.UsedAlias = true;
                            return commandHelp;
                        }
                    }
                }
            }

            return new CommandHelp(World, sender, CommandList, null);
        }
    }
}
