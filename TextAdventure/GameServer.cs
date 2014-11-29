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
    public class GameServer
    {
        const int DEFAULT_PORT = 53251;
        public static UdpClient Client;

        public GameServer()
        {
            Client = new UdpClient();
        }

        public void Start()
        {
            Output.Write("Generating world... ");
            GameWorld world = GameWorld.Generate();

            List<Command> commands = new List<Command>();
            commands.Add(new CommandHelp(world, null, commands, null));
            commands.Add(new CommandStatus(world, null, null));
            commands.Add(new CommandLook(world, null, null));
            commands.Add(new CommandMove(world, null, null));
            commands.Add(new CommandInventory(world, null, null));
            commands.Add(new CommandTake(world, null, null));
            commands.Add(new CommandDrop(world, null, null));
            commands.Add(new CommandQuit(world, null));

            CommandParser parser = new CommandParser(commands);

            Player player = new Player();
            world.Players.Add(player);
            world.Map.EntryNode.Entities.Add(player);

            Output.WriteLine("Done.");

            Task listenTask = new Task(() => { Listen(parser, player); });
            listenTask.Start();

            Output.WriteLine("Listening...");

            listenTask.Wait();
        }

        private static void Listen(CommandParser parser, Player player)
        {
            UdpClient listener = new UdpClient(DEFAULT_PORT);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, DEFAULT_PORT);

            while (true)
            {
                byte[] bytes = listener.Receive(ref endPoint);

                string message = Encoding.Unicode.GetString(bytes);

                try
                {
                    ICommand command = parser.Parse(player, message);

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

                Output.WriteLine();

                byte[] sendBytes = Encoding.Unicode.GetBytes("Got command.");
                Client.Send(sendBytes, sendBytes.Length, endPoint);
            }
        }
    }
}
