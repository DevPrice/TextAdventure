using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureClient
{
    class TextAdventureClient
    {
        const int DEFAULT_PORT = 53251;
        static UdpClient Client;

        static void Main(string[] args)
        {
            Client = new UdpClient(0);
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, DEFAULT_PORT);

            Task listenTask = new Task(Listen);
            listenTask.Start();
            
            while (true)
            {
                byte[] bytes = Encoding.Unicode.GetBytes(Console.ReadLine());
                Client.Send(bytes, bytes.Length, serverEndPoint);
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
