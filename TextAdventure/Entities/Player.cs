using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Commands;
using TextAdventure.World;

namespace TextAdventure.Entities
{
    public class Player : Entity, ICommandSender
    {
        public CommandPermission Permission { get; set; }

        public Player()
        {
            Name = "You";
        }
    }
}
