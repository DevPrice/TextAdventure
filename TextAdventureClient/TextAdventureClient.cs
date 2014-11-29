using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TextAdventureClient
{
    class TextAdventureClient
    {
        const int DEFAULT_PORT = 53251;
        static UdpClient Client;
        static Queue<string> MessageQueue;

        static void Main(string[] args)
        {
            IPEndPoint serverEndPoint = null;

            while (Client == null || serverEndPoint == null)
            {
                try
                {
                    Console.Write("Enter server IP: ");
                    string serverIp = Console.ReadLine();

                    Client = new UdpClient(0);
                    serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIp), DEFAULT_PORT);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid IP address.");
                }
            }

            byte[] connectBytes = Encoding.Unicode.GetBytes(String.Empty);
            Client.Send(connectBytes, connectBytes.Length, serverEndPoint);

            Console.WriteLine("Connected.");

            Task listenTask = new Task(Listen);
            listenTask.Start();
            
            while (true)
            {
                Console.WriteLine();
                Console.Write(">");
                string command = Console.ReadLine();
                byte[] bytes = Encoding.Unicode.GetBytes(command);
                Client.Send(bytes, bytes.Length, serverEndPoint);
                Thread.Sleep(150);
            }
        }

        private static void Listen()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, DEFAULT_PORT);

            while (true)
            {
                byte[] bytes = Client.Receive(ref endPoint);

                string message = Encoding.Unicode.GetString(bytes);

                Console.WriteLine(message);
            }
        }
    }
}
