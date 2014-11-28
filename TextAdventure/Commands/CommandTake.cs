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

                node.Items.Remove(Target);

                ((Player)Sender).Inventory.Add(Target);

                Output.WriteLine("You take the {0}.", Target.Name);
            }
            else
            {
                Output.WriteLine("You can't take that.");
            }
        }

        public override ICommand Create(ICommandSender sender, string[] args)
        {
            if (sender is Player && args.Length >= 2)
            {
                string[] itemNameArr = new string[args.Length - 1];
                Array.Copy(args, itemNameArr, itemNameArr.Length);
                string itemName = String.Join(" ", itemNameArr);

                IMapNode node = World.Map.LocationOf((Player)sender);

                Item takenItem = null;

                foreach (Item item in node.Items)
                {
                    if (item.Name.Equals(itemName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        takenItem = item;
                        break;
                    }
                }

                return new CommandTake(World, sender, takenItem);
            }

            return new CommandTake(World, sender, null);
        }
    }
}
