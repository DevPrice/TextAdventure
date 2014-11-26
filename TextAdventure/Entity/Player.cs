using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Command;
using TextAdventure.World;

namespace TextAdventure.Entity
{
    public class Player : EntityBase, ICommandSender
    {
        public CommandPermission Permission { get; set; }

        public Player()
        {
            Name = "You";
        }
    }
}
