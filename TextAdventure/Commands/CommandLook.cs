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

            Target.Examine();

            if (Target is IMapNode)
            {
                Sender.SendMessage();

                ListItems();

                foreach (Path path in World.Map.GetPathsFrom((IMapNode)Target))
                {
                    path.Examine();
                }
            }
        }

        private void ListItems()
        {
            foreach (Item item in ((IMapNode)Target).Items)
            {
                Output.Write("You see a {0} on the ground.", item.Name);
            }

            if (((IMapNode)Target).Items.Count > 0)
            {
                Sender.SendMessage();
                Sender.SendMessage();
            }
        }

        private void ListEntities()
        {
            foreach (Entity entity in ((IMapNode)Target).Entities)
            {
                Output.Write("You see a {0}.", entity.Name);
            }

            if (((IMapNode)Target).Entities.Count > 0)
            {
                Sender.SendMessage();
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

                foreach (Item item in currentNode.Items)
                {
                    if (item.Name.Equals(args[1], StringComparison.CurrentCultureIgnoreCase))
                        return new CommandLook(World, sender, item);
                }
            }

            throw new UsageException();
        }
    }
}
