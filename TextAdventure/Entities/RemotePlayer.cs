using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.World;

namespace TextAdventure.Entities
{
    public class RemotePlayer : Player
    {
        public UdpClient Client { get; private set; }
        public IPEndPoint EndPoint { get; private set; }

        private StringBuilder MessageBuffer { get; set; }

        public RemotePlayer(GameWorld world, UdpClient client, IPEndPoint endPoint)
            : base(world)
        {
            Client = client;
            EndPoint = endPoint;

            MessageBuffer = new StringBuilder();
        }

        public override void Update(TimeSpan delta)
        {
            base.Update(delta);

            Flush();
        }

        public override void Send(string message)
        {
            if (message == null)
                return;

            if (!Alive && !message.Contains("YOU DIED"))
                return;

            MessageBuffer.Append(message);
        }

        private void Flush()
        {
            if (MessageBuffer.Length == 0)
                return;

            byte[] bytes = Encoding.Unicode.GetBytes(MessageBuffer.ToString());
            Client.Send(bytes, bytes.Length, EndPoint);

            MessageBuffer.Clear();
        }
    }
}
