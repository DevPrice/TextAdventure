using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Entities
{
    public class RemotePlayer : Player
    {
        public IPEndPoint EndPoint { get; private set; }

        public RemotePlayer(IPEndPoint endPoint)
        {
            EndPoint = endPoint;
        }

        public override void SendMessage(string message)
        {

        }
    }
}
