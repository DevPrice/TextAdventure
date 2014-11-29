using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Commands
{
    public class CommandParser
    {
        public IEnumerable<Command> Commands { get; private set; }

        public CommandParser(IEnumerable<Command> commands)
        {
            Commands = commands;
        }

        public ICommand Parse(string command, ICommandSender sender)
        {
            if (command == null)
                return null;

            command = command.Trim();
            string[] args = command.Split(' ');
            string commandName = args[0];

            ICommandFactory commandFactory = GetCommandByName(commandName);

            if (commandFactory != null)
                return commandFactory.Create(sender, args);

            List<Command> factoriesFound = GetCommandsByAlias(commandName);

            if (factoriesFound.Count == 1)
                return factoriesFound[0].Create(sender, args);

            if (factoriesFound.Count == 0)
                throw new CommandNotFoundException(commandName);
            else
                throw new SharedAliasException(commandName);
        }

        private Command GetCommandByName(string name)
        {
            foreach (Command command in Commands)
            {
                if (command.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
                    return command;
            }

            return null;
        }

        private List<Command> GetCommandsByAlias(string name)
        {
            List<Command> commandsFound = new List<Command>();

            foreach (Command command in Commands)
            {
                foreach (string alias in command.Aliases)
                {
                    if (alias.Equals(name, StringComparison.CurrentCultureIgnoreCase))
                        commandsFound.Add(command);
                }
            }

            return commandsFound;
        }
    }
}
