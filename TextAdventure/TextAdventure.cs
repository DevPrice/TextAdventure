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
            GameWorld world = GameWorld.Generate();

            Player player = new Player();
            world.Players.Add(player);
            world.Map.EntryNode.Entities.Add(player);

            List<Command> commands = new List<Command>();
            commands.Add(new CommandHelp(world, null, commands, null));
            commands.Add(new CommandStatus(world, null, null));
            commands.Add(new CommandLook(world, null, null));
            commands.Add(new CommandMove(world, null, null));
            commands.Add(new CommandInventory(world, null, null));
            commands.Add(new CommandTake(world, null, null));
            commands.Add(new CommandQuit(world, null));

            CommandParser parser = new CommandParser(commands);

            while (true)
            {
                Output.WriteLine();
                Output.Write(">");

                try
                {
                    ICommand command = parser.Parse(player, Console.ReadLine());

                    try
                    {
                        command.Execute();
                    }
                    catch (UsageException)
                    {
                        Output.WriteLine("Invalid usage.");
                    }
                    catch (InsufficientPermissionException)
                    {
                        Output.WriteLine("Invalid permissions.");
                    }
                }
                catch (CommandNotFoundException)
                {
                    Output.WriteLine("Invalid command.");
                }
            }
        }
    }
}
