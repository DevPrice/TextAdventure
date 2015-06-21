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
    public class CommandTake : Command<Item>
    {
        public CommandTake(GameWorld world, ICommandSender sender, Item item)
            : base(world, sender, item)
        {
            Name = "take";
            Aliases.Add("get");
            Aliases.Add("grab");
            Aliases.Add("pickup");
            Aliases.Add("pick");
            Aliases.Add("steal");
            Aliases.Add("snatch");
        }

        public override void Execute()
        {
            base.Execute();

            if (Target != null)
            {
                IMapNode node = World.Map.LocationOf(Target);

                node.Remove(Target);

                ((Player)Sender).Inventory.Add(Target);
                Sender.SendLine("You take the {0}.", Target.Name);
                Target.OnPickup((Entity)Sender);
            }
            else
            {
                Sender.SendLine("You can't take that.");
            }
        }

        public override ICommand Create(ICommandSender sender, string[] args)
        {
            if (sender is Player && args.Length >= 2)
            {
                string[] itemNameArr = new string[args.Length - 1];
                Array.Copy(args, 1, itemNameArr, 0, itemNameArr.Length);
                string itemName = String.Join(" ", itemNameArr);

                IMapNode node = World.Map.LocationOf((Player)sender);

                Item takenItem = node.Items.GetByName(itemName);

                return new CommandTake(World, sender, takenItem);
            }

            return new CommandTake(World, sender, null);
        }
    }
}
