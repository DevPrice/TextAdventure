﻿using System;
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
    public class CommandDrop : Command<Item>
    {
        public CommandDrop(GameWorld world, ICommandSender sender, Item item)
            : base(world, sender, item)
        {
            Name = "drop";
            Description = "Drops an item you are carrying.";
            Usage = "drop [item]";
        }

        public override void Execute()
        {
            base.Execute();

            if (Target != null)
            {
                IMapNode currentNode = World.Map.LocationOf(((Player)Sender));

                ((Player)Sender).Inventory.Remove(Target);
                currentNode.Add(Target);

                Sender.SendLine("Stealth cries to Coldplay.");
                Sender.SendLine("You drop the {0}.", Target.Name);
                
            }
            else
            {
                Sender.SendLine("You can't drop that.");
            }
        }

        public override ICommand Create(ICommandSender sender, string[] args)
        {
            if (sender is Player && args.Length >= 2)
            {
                string[] itemNameArr = new string[args.Length - 1];
                Array.Copy(args, 1, itemNameArr, 0, itemNameArr.Length);
                string itemName = String.Join(" ", itemNameArr);

                Item droppedItem = ((Player)sender).Inventory.GetByName(itemName);

                return new CommandDrop(World, sender, droppedItem);
            }

            return new CommandDrop(World, sender, null);
        }
    }
}
