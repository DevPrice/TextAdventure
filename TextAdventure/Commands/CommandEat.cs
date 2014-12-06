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
    public class CommandEat : Command<IEdible>
    {
        public CommandEat(GameWorld world, ICommandSender sender, IEdible item)
            : base(world, sender, item)
        {
            Name = "eat";
            Aliases.Add("drink");
            Aliases.Add("swallow");
            Aliases.Add("consume");
            Aliases.Add("ingest");
            Description = "Eat an edible item.";
            Usage = "eat [food]";
        }

        public override void Execute()
        {
            base.Execute();

            if (Sender is Entity)
            {
                Entity entity = (Entity)Sender;
                
                Target.Eat(entity);
                entity.Inventory.Remove(Target);
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
                string[] foodNameArr = new string[args.Length - 1];
                Array.Copy(args, 1, foodNameArr, 0, foodNameArr.Length);
                string foodName = string.Join(" ", foodNameArr);

                IEdible food = ((Entity)sender).Inventory.GetByName(foodName) as IEdible;

                return new CommandEat(World, sender, food);
            }

            throw new UsageException();
        }
    }
}
