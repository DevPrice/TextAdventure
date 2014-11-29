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
    public class GameServer : ICommandSender
    {
        const int DEFAULT_PORT = 53251;
        public static UdpClient Client { get; private set; }
        public static List<RemotePlayer> Players { get; private set; }
        public GameWorld World { get; private set; }
        public CommandEngine CommandEngine { get; private set; }
        private int PlayerNum;

        public GameServer()
        {
            Client = new UdpClient(DEFAULT_PORT);
            Players = new List<RemotePlayer>();
            PlayerNum = 1;
        }

        public void Start()
        {
            Output.Write("Generating world... ");
            World = GameWorld.Generate();

            List<Command> commands = new List<Command>();
            commands.Add(new CommandHelp(World, null, commands, null));
            commands.Add(new CommandStatus(World, null, null));
            commands.Add(new CommandLook(World, null, null));
            commands.Add(new CommandMove(World, null, null));
            commands.Add(new CommandInventory(World, null, null));
            commands.Add(new CommandTake(World, null, null));
            commands.Add(new CommandDrop(World, null, null));
            commands.Add(new CommandQuit(World, null));

            CommandParser parser = new CommandParser(commands);
            CommandEngine = new CommandEngine(parser);

            Output.WriteLine("Done.");

            Task listenTask = new Task(Listen);
            listenTask.Start();

            Output.WriteLine("Listening...");

            while (true)
            {
                Output.Write(">");
                CommandEngine.RunCommand(Console.ReadLine(), this);
            }
        }

        private void Listen()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, DEFAULT_PORT);

            while (true)
            {
                byte[] bytes = Client.Receive(ref endPoint);

                RemotePlayer player = GetPlayerFromEndPoint(endPoint);

                if (player == null)
                {
                    player = new RemotePlayer(Client, endPoint);
                    Players.Add(player);
                    World.Players.Add(player);
                    World.Map.EntryNode.Entities.Add(player);

                    player.Name = String.Format("Player {0}", PlayerNum++);

                    Output.WriteLine("Player connected from {0}:{1}", endPoint.Address, endPoint.Port);
                }

                string message = Encoding.Unicode.GetString(bytes);

                if (message.Length > 0)
                    CommandEngine.RunCommand(message, player);
            }
        }

        private static RemotePlayer GetPlayerFromEndPoint(IPEndPoint endPoint)
        {
            foreach (RemotePlayer player in Players)
            {
                if (player.EndPoint.Equals(endPoint))
                    return player;
            }

            return null;
        }

        public CommandPermission Permission
        {
            get { return CommandPermission.Admin; }
        }

        public void SendMessage()
        {
            SendMessage("");
        }

        public void SendMessage(string message)
        {
            Output.WriteLine(message);
        }

        public void SendMessage(string message, params object[] args)
        {
            SendMessage(String.Format(message, args));
        }
    }
}
