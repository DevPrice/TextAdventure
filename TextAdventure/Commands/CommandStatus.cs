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
        public CommandStatus(GameWorld world, ICommandSender sender)
            : base(world, sender)
        {
            Name = "status";
            Aliases.Add("hp");
        }

        public override void Execute()
        {
            base.Execute();

            if (Sender is Player)
                Output.WriteLine("HP: {0}", ((Player)Sender).Hp);
        }

        public override ICommand Create(ICommandSender sender, string[] args)
        {
            return new CommandStatus(World, sender);
        }
    }
}
