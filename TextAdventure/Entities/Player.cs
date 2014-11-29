using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Behaviors;
using TextAdventure.Commands;
using TextAdventure.Utility;
using TextAdventure.World;

namespace TextAdventure.Entities
{
    public class Player : Entity, ICommandSender
    {
        public CommandPermission Permission { get; set; }

        public Player(GameWorld world)
            : base(world, 10)
        {
            Name = "You";
            Permission = CommandPermission.User;
        }

        public virtual void SendMessage(string message)
        {
            Output.WriteLine(message);
        }

        public void SendMessage()
        {
            SendMessage(String.Empty);
        }

        public void SendMessage(string value, params object[] args)
        {
            SendMessage(String.Format(value, args));
        }
    }
}
