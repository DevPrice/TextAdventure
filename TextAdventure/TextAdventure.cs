using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
        const int DEFAULT_PORT = 53251;
        static UdpClient Sender;

        static void Main(string[] args)
        {
            GameServer server = new GameServer();
            server.Start();

            /*while (player.Hp > 0)
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

            Output.WriteLine();
            Output.Write("YOU ARE DEAD");
            Console.ReadKey(false);*/
        }
    }
}
