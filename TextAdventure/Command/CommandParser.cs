using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Command
{
    class CommandParser
    {
        public IEnumerable<ICommand> Commands { get; private set; }

        public CommandParser(IEnumerable<ICommand> commands)
        {
            Commands = commands;
        }

        public ICommand Parse(string command)
        {
            if (command == null)
                return null;

            command = command.Trim();
            string[] args = command.Split(' ');
            string commandName = args[0];

            ICommand commandObj = GetCommandByName(commandName);

            if (commandObj != null)
                return commandObj.Create(args);

            List<ICommand> commandsFound = GetCommandsByAlias(commandName);

            if (commandsFound.Count == 1)
                return commandsFound[0].Create(args);

            if (commandsFound.Count == 0)
                throw new CommandNotFoundException(commandName);
            else
                throw new SharedAliasException(commandName);
        }

        private ICommand GetCommandByName(string name)
        {
            foreach (ICommand command in Commands)
            {
                if (command.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
                    return command;
            }

            return null;
        }

        private List<ICommand> GetCommandsByAlias(string name)
        {
            List<ICommand> commandsFound = new List<ICommand>();

            foreach (ICommand command in Commands)
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
