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
    public class CommandLook : Command<IExaminable>
    {
        public CommandLook(GameWorld world, ICommandSender sender, IExaminable lookAt)
            : base(world, sender, lookAt)
        {
            Name = "look";
            Aliases.Add("examine");
            Aliases.Add("view");

            Description = "Look at your surroundings or at a particular object of interest.";
            Usage = "look; look [item]; look [entity]";
        }

        public override void Execute()
        {
            base.Execute();

            Target.Examine(Sender);

            if (Target is IMapNode)
            {
                Sender.SendMessage();

                ListItems();
                ListEntities();

                foreach (Path path in World.Map.GetPathsFrom((IMapNode)Target))
                {
                    path.Examine(Sender);
                }
            }
        }

        private void ListItems()
        {
            foreach (Item item in ((IMapNode)Target).Items)
            {
                Sender.SendMessage("You see a [{0}] on the ground.", item.Name);
            }

            if (((IMapNode)Target).Items.Count > 0)
            {
                Sender.SendMessage();
            }
        }

        private void ListEntities()
        {
            foreach (Entity entity in ((IMapNode)Target).Entities)
            {
                if (entity != Sender as Entity)
                    Sender.SendMessage("You see{0} [{1}].", entity is Player ? "" : " a", entity.Name);
            }

            if (((IMapNode)Target).Entities.Count > 1)
            {
                Sender.SendMessage();
            }
        }
        
        public override ICommand Create(ICommandSender sender, string[] args)
        {
            if (sender is Player)
            {
                IMapNode currentNode = World.Map.LocationOf((Player)sender);

                if (args.Length < 2)
                    return new CommandLook(World, sender, currentNode);

                IExaminable examinable = ((Player)sender).Inventory.GetByName(args[1]);

                if (examinable == null)
                    examinable = currentNode.Items.GetByName(args[1]);

                if (examinable == null)
                    examinable = currentNode.Entities.GetByName(args[1]);

                if (examinable != null)
                    return new CommandLook(World, sender, examinable);
            }

            throw new UsageException();
        }
    }
}
