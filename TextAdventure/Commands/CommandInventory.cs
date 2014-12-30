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
    public class CommandInventory : Command<Player>
    {
        public CommandInventory(GameWorld world, ICommandSender sender, Player player)
            : base(world, sender, player)
        {
            Name = "inventory";
            Aliases.Add("inv");
            Aliases.Add("items");

            Description = "Displays your current inventory.";
            Usage = "inventory";
        }

        public override void Execute()
        {
            base.Execute();

            if (Target != null)
            {
                foreach (Item item in Target.Inventory)
                {
                    Sender.Send("{0} ", item.Name);
                }
            }

            Sender.SendLine();
        }

        public override ICommand Create(ICommandSender sender, string[] args)
        {
            if (sender is Player)
            {
                return new CommandInventory(World, sender, (Player)sender);
            }

            return new CommandInventory(World, sender, null);
        }
    }
}
