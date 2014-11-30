﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TextAdventure.Commands;
using TextAdventure.Entities;
using TextAdventure.Utility;
using TextAdventure.World;

namespace TextAdventure
{
    public class GameServer : ICommandSender, IUpdatable
    {
        public const int DEFAULT_PORT = 53251;
        public const int BROADCAST_PORT = 15235;

        public int Port { get; private set; }
        public UdpClient Client { get; private set; }
        public List<RemotePlayer> Players { get; private set; }
        public GameWorld World { get; private set; }
        public CommandEngine CommandEngine { get; private set; }
        public int PlayerNum { get; private set; }
        private volatile bool _Running;
        public bool Running { get { return _Running; } private set { _Running = value; } }
        public DateTime LastUpdate { get; private set; }
        public readonly int TickRate;

        public GameServer()
        {
            Players = new List<RemotePlayer>();
            PlayerNum = 1;
            Port = DEFAULT_PORT;
            TickRate = 20;
        }

        public GameServer(int port)
            : this()
        {
            Port = port;
        }

        public void Start()
        {
            if (Running)
                throw new InvalidOperationException();

            Running = true;

            Client = new UdpClient(Port);

            Output.Write("Generating world... ");
            World = GameWorld.Generate();

            List<Command> commands = new List<Command>();
            commands.Add(new CommandHelp(World, null, commands, null));
            commands.Add(new CommandSay(World, null, String.Empty));
            commands.Add(new CommandStatus(World, null, null));
            commands.Add(new CommandLook(World, null, null));
            commands.Add(new CommandMove(World, null, null));
            commands.Add(new CommandInventory(World, null, null));
            commands.Add(new CommandTake(World, null, null));
            commands.Add(new CommandDrop(World, null, null));
            commands.Add(new CommandAttack(World, null, null));
            commands.Add(new CommandQuit(World, null, this));

            CommandParser parser = new CommandParser(commands);
            CommandEngine = new CommandEngine(parser);

            Output.WriteLine("Done.");

            Task updateTask = new Task(BeginUpdate);
            updateTask.Start();

            Task broadcastTask = new Task(Broadcast);
            broadcastTask.Start();

            Task listenTask = new Task(Listen);
            listenTask.Start();

            Output.WriteLine("Listening...");

            while (Running)
            {
                Output.Write(">");
                CommandEngine.RunCommand(Console.ReadLine(), this);
            }
        }

        public void Stop()
        {
            Running = false;
        }

        private void BeginUpdate()
        {
            while (Running)
            {
                DateTime now = DateTime.Now;
                Update(now - LastUpdate);
                LastUpdate = now;
                Thread.Sleep(1000 / TickRate);
            }
        }

        public void Update(TimeSpan delta)
        {
            World.Update(delta);
        }

        private void Listen()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, Port);

            while (Running)
            {
                try
                {
                    byte[] bytes = Client.Receive(ref endPoint);

                    RemotePlayer player = GetPlayerFromEndPoint(endPoint);

                    if (player == null)
                    {
                        player = new RemotePlayer(World, Client, endPoint);
                        Players.Add(player);
                        World.Players.Add(player);
                        World.Map.EntryNode.Entities.Add(player);

                        player.Name = String.Format("Player{0}", PlayerNum++);

                        Output.WriteLine("Player connected from {0}:{1}.", endPoint.Address, endPoint.Port);
                    }

                    string message = Encoding.Unicode.GetString(bytes);

                    if (message.Length > 0 && player.Alive)
                        CommandEngine.RunCommand(message, player);
                }
                catch (SocketException)
                {

                }
            }
        }

        private void Broadcast()
        {
            while (Running)
            {
                byte[] bytes = Encoding.Unicode.GetBytes("CTRL:BROADCAST");
                Client.Send(bytes, bytes.Length, new IPEndPoint(IPAddress.Broadcast, BROADCAST_PORT));

                Thread.Sleep(1000);
            }
        }

        private RemotePlayer GetPlayerFromEndPoint(IPEndPoint endPoint)
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
            SendMessage(String.Empty);
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
