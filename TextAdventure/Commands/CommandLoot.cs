using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;
using TextAdventure.Items;
using TextAdventure.World;
using TextAdventure.Utility;

namespace TextAdventure.Commands
{
    public class CommandLoot : Command<Entity>
    {
        public CommandLoot(GameWorld world, ICommandSender sender, Entity target)
            : base(world, sender, target)
        {
            Name = "loot";
            Aliases.Add("search");
        }

        public override void Execute()
        {
            base.Execute();

            Player player = Sender as Player;

            if (player == null || Target == null || Target.Alive)
                throw new UsageException();

            IEnumerable<Item> items = new List<Item>(Target.Inventory);
            foreach (Item item in items)
            {
                Target.Inventory.Remove(item);
                player.Inventory.Add(item);

                player.SendLine("You take {0}.", item.GetFullName());
            }

            if (items.Count() == 0)
            {
                player.SendLine("You find nothing.");
            }
        }

        public override ICommand Create(ICommandSender sender, string[] args)
        {
            if (sender is Entity && args.Length >= 2)
            {
                string[] entityNameArr = new string[args.Length - 1];
                Array.Copy(args, 1, entityNameArr, 0, entityNameArr.Length);
                string entityName = String.Join(" ", entityNameArr);

                IMapNode node = World.Map.LocationOf((Player)sender);

                Entity lootTarget = null;

                foreach (Entity entity in node.Entities)
                {
                    if (entity.Name.Equals(entityName, StringComparison.CurrentCultureIgnoreCase) && !entity.Alive)
                        lootTarget = entity;
                }

                if (lootTarget == null)
                    throw new UsageException();

                return new CommandLoot(World, sender, lootTarget);
            }

            throw new UsageException();
        }
    }
}
