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
                commandObj.Execute();
            }
            catch (CommandNotFoundException)
            {
                sender.SendLine("Invalid command.");
            }
            catch (UsageException)
            {
                sender.SendLine("Invalid usage.");
            }
            catch (InsufficientPermissionException)
            {
                sender.SendLine("Invalid permissions.");
            }
        }
    }
}
