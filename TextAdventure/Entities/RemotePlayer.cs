using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Entities
{
    public class RemotePlayer : Player
    {
        public UdpClient Client { get; private set; }
        public IPEndPoint EndPoint { get; private set; }

        public RemotePlayer(UdpClient client, IPEndPoint endPoint)
        {
            Client = client;
            EndPoint = endPoint;
        }

        public override void SendMessage(string message)
        {
            if (message == null)
                return;

            byte[] bytes = Encoding.Unicode.GetBytes(message);
            Client.Send(bytes, bytes.Length, EndPoint);
        }
    }
}
