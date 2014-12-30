using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;
using TextAdventure.World;

namespace TextAdventure.Commands
{
    public class CommandDie : Command
    {
        public CommandDie(GameWorld world, ICommandSender sender)
            : base(world, sender)
        {
            Name = "die";
            Aliases.Add("suicide");
            Description = "Kill yourself.";
            Usage = "die";
            Hidden = true;
        }

        public override void Execute()
        {
            base.Execute();

            if (Sender is Entity)
            {
                Sender.SendLine("You crush your own skull with your {0} hands.", World.Random.Next(2) == 0 ? "bare" : "bear");
                ((Entity)Sender).DealDamage(DamageSource.World, Double.MaxValue);
            }
        }

        public override ICommand Create(ICommandSender sender, string[] args)
        {
            if (sender is Entity)
            {
                return new CommandDie(World, sender);
            }

            throw new UsageException();
        }
    }
}
