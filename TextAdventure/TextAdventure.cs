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
    class TextAdventure
    {
        static readonly string GameName = "TextAdventure";
        static void Main(string[] args)
        {
            Output.WriteLine("{0} Server", GameName);
            Output.WriteLine();

            GameServer server = new GameServer();
            server.Start();
        }
    }
}
