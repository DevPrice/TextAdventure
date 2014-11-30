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
        const int BROADCAST_PORT = 15235;
        static UdpClient Client;

        static void Main(string[] args)
        {
            Console.WriteLine("TextAdventure Client");
            Console.WriteLine();

            IPEndPoint serverEndPoint = null;

            while (Client == null || serverEndPoint == null)
            {
                UdpClient broadcastClient = new UdpClient(BROADCAST_PORT);
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, BROADCAST_PORT);

                byte[] bytes = broadcastClient.Receive(ref endPoint);

                string message = Encoding.Unicode.GetString(bytes);

                if (message.Equals("CTRL:BROADCAST"))
                    serverEndPoint = endPoint;

                broadcastClient.Close();

                Client = new UdpClient();
                
                /*try
                {
                    Console.Write("Enter server IP: ");
                    string serverIp = Console.ReadLine();

                    Client = new UdpClient(0, AddressFamily.InterNetworkV6);
                    IPAddress[] addresses = Dns.GetHostAddresses(serverIp);
                    serverEndPoint = new IPEndPoint(addresses[0], DEFAULT_PORT);
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid host address.");
                }*/
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

            try
            {
                while (true)
                {
                    byte[] bytes = Client.Receive(ref endPoint);

                    string message = Encoding.Unicode.GetString(bytes);

                    if (!message.StartsWith("CTRL:"))
                        Console.WriteLine(message);
                }
            }
            catch (SocketException)
            {
                Console.WriteLine("Error: Disconnected from server.");
            }
        }
    }
}
