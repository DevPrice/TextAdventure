using System;
using System.Collections;
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
    public class CommandUnequip : Command<Item>
    {
        public CommandUnequip(GameWorld world, ICommandSender sender, Item item)
            : base(world, sender, item)
        {
            Name = "unequip";
            Aliases.Add("remove");
            Description = "Unequip an item.";
            Usage = "unequip [item]";
        }

        public override void Execute()
        {
            base.Execute();

            if (Sender is Entity)
            {
                Entity entity = (Entity)Sender;

                if (Target is ItemWieldable)
                {
                    entity.Equipment.Remove((ItemWieldable)Target);
                }
                else
                {
                    throw new UsageException();
                }
            }
            else
            {
                throw new UsageException();
            }
        }

        public override ICommand Create(ICommandSender sender, string[] args)
        {
            if (sender is Entity)
            {
                string[] itemNameArr = new string[args.Length - 1];
                Array.Copy(args, 1, itemNameArr, 0, itemNameArr.Length);
                string itemName = string.Join(" ", itemNameArr);

                Item item = ((Entity)sender).Equipment.GetByName(itemName);

                return new CommandUnequip(World, sender, item);
            }

            throw new UsageException();
        }
    }
}
