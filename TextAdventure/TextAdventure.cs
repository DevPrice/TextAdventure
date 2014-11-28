using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Commands;
using TextAdventure.Entities;
using TextAdventure.Utility;
using TextAdventure.World;

namespace TextAdventure
{
    class TextAdventure
    {
        static void Main(string[] args)
        {
            GameWorld world = new GameWorld();

            List<Command> commands = new List<Command>();
            commands.Add(new CommandHelp(world, commands));
            commands.Add(new CommandStatus(world));
            commands.Add(new CommandQuit(world));

            CommandParser parser = new CommandParser(commands);

            Player player = new Player();

            while (true)
            {
                Output.WriteLine();
                Output.Write(">");

                try
                {
                    ICommand command = parser.Parse(Console.ReadLine());

                    try
                    {
                        command.Execute(player);
                    }
                    catch (UsageException)
                    {
                        Output.WriteLine("Invalid usage.");
                    }
                }
                catch (CommandNotFoundException)
                {
                    Output.WriteLine("Invalid command.");
                }
                catch (InsufficientPermissionException)
                {
                    Output.WriteLine("Invalid permissions.");
                }
            }
        }
    }
}
