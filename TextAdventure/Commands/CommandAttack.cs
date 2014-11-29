using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;
using TextAdventure.World;

namespace TextAdventure.Commands
{
    public class CommandAttack : Command<Entity>
    {
        public CommandAttack(GameWorld world, ICommandSender sender, Entity entity)
            : base(world, sender, entity)
        {
            Name = "attack";
            Aliases.Add("fight");
            Aliases.Add("hit");
            Aliases.Add("slap");
            Aliases.Add("punch");
            Aliases.Add("kick");
        }

        public override void Execute()
        {
            base.Execute();

            ((Entity)Sender).CombatTarget = Target;
        }

        public override ICommand Create(ICommandSender sender, string[] args)
        {
            if (sender is Entity && args.Length >= 2)
            {
                string[] itemNameArr = new string[args.Length - 1];
                Array.Copy(args, 1, itemNameArr, 0, itemNameArr.Length);
                string entityName = String.Join(" ", itemNameArr);

                IMapNode node = World.Map.LocationOf((Player)sender);

                Entity attackTarget = null;

                foreach (Entity entity in node.Entities)
                {
                    if (entity.Name.Equals(entityName, StringComparison.CurrentCultureIgnoreCase)
                        && entity != sender)
                    {
                        attackTarget = entity;
                        break;
                    }
                }

                if (attackTarget == null)
                    throw new UsageException();

                return new CommandAttack(World, sender, attackTarget);
            }

            throw new UsageException();
        }
    }
}
