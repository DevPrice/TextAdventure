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
    public class CommandEquip : Command<Item>
    {
        public CommandEquip(GameWorld world, ICommandSender sender, Item item)
            : base(world, sender, item)
        {
            Name = "equip";
            Aliases.Add("wear");
            Aliases.Add("hold");
            Description = "Equip an item.";
            Usage = "equip [item]";
        }

        public override void Execute()
        {
            base.Execute();

            if (Sender is Entity)
            {
                Entity entity = (Entity)Sender;

                if (Target is ItemWieldable)
                {
                    entity.Equipment.Add((ItemWieldable)Target);
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

                Item item = ((Entity)sender).Inventory.GetByName(itemName);

                return new CommandEquip(World, sender, item);
            }

            throw new UsageException();
        }
    }
}
