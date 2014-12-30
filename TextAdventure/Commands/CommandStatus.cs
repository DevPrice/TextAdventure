using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;
using TextAdventure.Items;
using TextAdventure.Utility;
using TextAdventure.World;

namespace TextAdventure.Commands
{
    public class CommandStatus : Command<Entity>
    {
        public CommandStatus(GameWorld world, ICommandSender sender, Entity entity)
            : base(world, sender, entity)
        {
            Name = "status";
            Aliases.Add("hp");
        }

        public override void Execute()
        {
            base.Execute();

            int currentLevelXp = ((Player)Target).Experience - Experience.GetTotalExpForLevel(((Player)Target).Level);
            int nextLevelXp = Experience.NeededForLevel[((Player)Target).Level + 1];

            if (Target is Player)
            {
                Sender.SendMessage(((Player)Sender).Name);
                Sender.SendMessage();
                Sender.SendMessage("Level: {0}, {1}/{2}xp", ((Player)Target).Level, currentLevelXp, nextLevelXp);
            }

            Sender.SendMessage("HP: {0}/{1}", Math.Ceiling(Target.Hp), Target.Attributes.MaxHp);
            Sender.SendMessage();

            foreach (ItemWieldable item in Target.Equipment)
            {
                Sender.SendMessage("{0}: {1}", item.Slot.ToString(), item.Name);
            }
        }

        public override ICommand Create(ICommandSender sender, string[] args)
        {
            if (sender is Player)
                return new CommandStatus(World, sender, (Entity)sender);

            throw new UsageException();
        }
    }
}
