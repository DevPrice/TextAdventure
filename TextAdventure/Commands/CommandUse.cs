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
    class CommandUse : Command<Item>
    {
        public CommandUse(GameWorld world, ICommandSender sender, Item item)
            : base(world, sender, item)
        {
            Name = "use";
            Description = "Use an item.";
            Usage = "use [item]";
        }

        public override void Execute()
        {
            base.Execute();

            if (Sender is Entity)
            {
                Entity entity = (Entity)Sender;

                if (Target is IUsable)
                {
                    ((IUsable)Target).Use(entity);
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

                return new CommandUse(World, sender, item);
            }

            throw new UsageException();
        }
    }
}
