using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;
using TextAdventure.World;

namespace TextAdventure.Commands
{
    public class CommandTruce : Command
    {
        public CommandTruce(GameWorld world, ICommandSender sender)
            : base(world, sender)
        {
            Name = "truce";
            Aliases.Add("yield");
            Description = "Stop combat.";
            Usage = "truce";
        }

        public override void Execute()
        {
            base.Execute();

            if (Sender is Entity)
            {
                ((Entity)Sender).CombatTarget = null;
            }
        }

        public override ICommand Create(ICommandSender sender, string[] args)
        {
            return new CommandTruce(World, sender);
        }
    }
}
