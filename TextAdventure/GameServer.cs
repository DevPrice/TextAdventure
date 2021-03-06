﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TextAdventure.Commands;
using TextAdventure.Entities;
using TextAdventure.Items;
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
        public List<RemotePlayer> RemotePlayers { get; private set; }
        public GameWorld World { get; private set; }
        public CommandEngine CommandEngine { get; private set; }
        public int PlayerNum { get; private set; }
        private volatile bool _Running;
        public bool Running { get { return _Running; } private set { _Running = value; } }
        public DateTime LastUpdate { get; private set; }
        public bool UseRealTime { get; protected set; }
        public readonly int TickRate;
        public readonly TimeSpan TickLength;

        public GameServer()
        {
            RemotePlayers = new List<RemotePlayer>();
            PlayerNum = 1;
            Port = DEFAULT_PORT;
            TickRate = 20;
            TickLength = TimeSpan.FromMilliseconds(1000 / TickRate);
        }

        public GameServer(int port)
            : this()
        {
            Port = port;
        }

        public void Start()
        {
            if (Running)
                throw new InvalidOperationException("Server already started.");

            Running = true;
            Client = new UdpClient(Port);

            Output.Write("Generating world... ");

            World = GameWorld.Generate();

            Output.WriteLine("Done.");

            Output.Write("Starting command engine... ");

            List<Command> commands = new List<Command>();
            commands.Add(new CommandHelp(World, null, commands, null));
            commands.Add(new CommandSay(World, null, String.Empty));
            commands.Add(new CommandStatus(World, null, null));
            commands.Add(new CommandLook(World, null, null));
            commands.Add(new CommandMove(World, null, null));
            commands.Add(new CommandInventory(World, null, null));
            commands.Add(new CommandTake(World, null, null));
            commands.Add(new CommandDrop(World, null, null));
            commands.Add(new CommandEquip(World, null, null));
            commands.Add(new CommandUnequip(World, null, null));
            commands.Add(new CommandUse(World, null, null));
            commands.Add(new CommandEat(World, null, null));
            commands.Add(new CommandAttack(World, null, null));
            commands.Add(new CommandTruce(World, null));
            commands.Add(new CommandLoot(World, null, null));
            commands.Add(new CommandDie(World, null));
            commands.Add(new CommandQuit(World, null));

            CommandParser parser = new CommandParser(commands);
            CommandEngine = new CommandEngine(parser);

            RantEngine.RunPattern(""); // Run a blank pattern to initialize the rant engine.
                                       // This avoids a discernible delay during gameplay if the engine is initialized later.

            Output.WriteLine("Done.");

            LastUpdate = DateTime.Now;
            Task updateTask = new Task(BeginUpdate);
            updateTask.Start();

            Task broadcastTask = new Task(Broadcast);
            broadcastTask.Start();

            Task listenTask = new Task(Listen);
            listenTask.Start();

            Output.WriteLine("Listening on port {0}...", Port);

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

                TimeSpan delta;

                if (UseRealTime)
                {
                    delta = now - LastUpdate;
                }
                else
                {
                    delta = TickLength;
                }

                Update(delta);
                LastUpdate = now;

                TimeSpan actualTickLength = DateTime.Now - now;

                if (actualTickLength < TickLength)
                    Thread.Sleep(TickLength - actualTickLength);
            }
        }

        public void Update(TimeSpan delta)
        {
            if (delta > TimeSpan.FromSeconds(1.5 / TickRate))
            {
                Output.WriteLine("Tick took {0}ms! Server could be overloaded.", delta.TotalMilliseconds);
            }

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

                        if (World.Random.NextDouble() < .5)
                            player.Gender = Gender.Female;

                        player.Name = RantEngine.RunPattern(String.Format("<name-{0}>", player.Gender.GetRantPattern()));

                        RemotePlayers.Add(player);
                        World.Players.Add(player);
                        World.Map.EntryNode.Add(player);
                        ItemMap map = new ItemMap((GridMap)World.Map);
                        player.Inventory.Add(map);
                        map.OnPickup(player);

                        Output.WriteLine("Player connected from {0}:{1}.", endPoint.Address, endPoint.Port);
                    }

                    string message = Encoding.Unicode.GetString(bytes);

                    if (message.Length > 0 && player.Alive)
                        CommandEngine.RunCommand(message, player);
                }
                catch (SocketException)
                {
                    Output.WriteLine("Player disconnected from {0}:{1}.", endPoint.Address, endPoint.Port);
                    RemotePlayer player = GetPlayerFromEndPoint(endPoint);

                    if (player != null)
                    {
                        RemotePlayers.Remove(player);
                    }
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
            foreach (RemotePlayer player in RemotePlayers)
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

        public void Send()
        {
            Send(String.Empty);
        }

        public void Send(string message)
        {
            Output.Write(message);
        }

        public void Send(string message, params object[] args)
        {
            Send(String.Format(message, args));
        }

        public void SendLine()
        {
            SendLine(String.Empty);
        }

        public void SendLine(string message)
        {
            Send(message + Environment.NewLine);
        }

        public void SendLine(string message, params object[] args)
        {
            SendLine(String.Format(message, args));
        }
    }
}
