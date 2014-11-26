using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Command;
using TextAdventure.Utility;
using TextAdventure.World;

namespace TextAdventure
{
    class TextAdventure
    {
        static void Main(string[] args)
        {
            List<ICommand> commands = new List<ICommand>();
            commands.Add(new CommandQuit());

            CommandParser parser = new CommandParser(commands);

            GameWorld world = new GameWorld();

            while (true)
            {
                Output.WriteLine();
                Output.Write(">");

                try
                {
                    ICommand command = parser.Parse(Console.ReadLine());
                    command.Execute();
                }
                catch (CommandNotFoundException)
                {
                    Output.WriteLine("Invalid command.");
                }
            }
        }
    }
}
