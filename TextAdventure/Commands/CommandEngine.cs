using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Commands
{
    public class CommandEngine
    {
        public CommandParser Parser { get; private set; }

        public CommandEngine(CommandParser parser)
        {
            Parser = parser;
        }

        public void RunCommand(string command, ICommandSender sender)
        {
            try
            {
                ICommand commandObj = Parser.Parse(command, sender);

                try
                {
                    commandObj.Execute();
                }
                catch (UsageException)
                {
                    sender.SendMessage("Invalid usage.");
                }
                catch (InsufficientPermissionException)
                {
                    sender.SendMessage("Invalid permissions.");
                }
            }
            catch (CommandNotFoundException)
            {
                sender.SendMessage("Invalid command.");
            }
        }
    }
}
