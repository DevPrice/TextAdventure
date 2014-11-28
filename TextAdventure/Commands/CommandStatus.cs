using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;
using TextAdventure.Utility;
using TextAdventure.World;

namespace TextAdventure.Commands
{
    public class CommandStatus : Command
    {
        public CommandStatus(GameWorld world)
            : base(world)
        {
            Name = "status";
            Aliases.Add("hp");
        }

        public override void Execute(ICommandSender sender)
        {
            base.Execute(sender);

            if (sender is Player)
                Output.WriteLine("HP: {0}", ((Player)sender).Hp);
        }

        public override ICommand Create(string[] args)
        {
            return new CommandStatus(World);
        }
    }
}
