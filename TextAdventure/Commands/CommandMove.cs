using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;
using TextAdventure.Utility;
using TextAdventure.World;

namespace TextAdventure.Commands
{
    public class CommandMove : Command<Path>
    {
        public CommandMove(GameWorld world, ICommandSender sender, Path path)
            : base(world, sender, path)
        {
            Name = "move";
            Aliases.Add("go");
            Aliases.Add("goto");

            Usage = "move [pathname]";
        }

        public override void Execute()
        {
            base.Execute();

            if (Target != null)
            {
                Target.From.Remove((Player)Sender);
                Target.To.Add((Player)Sender);

                Sender.SendMessage("You go {0}.", Target.Identifier);
            }
            else
            {
                Sender.SendMessage("You can't go that way.");
            }
        }

        public override ICommand Create(ICommandSender sender, string[] args)
        {
            if (sender is Player && args.Length >= 2)
            {
                IMapNode currentNode = World.Map.LocationOf((Player)sender);

                foreach (Path path in World.Map.GetPathsFrom(currentNode))
                {
                    if (path.Identifier.Equals(args[1], StringComparison.CurrentCultureIgnoreCase))
                    {
                        return new CommandMove(World, sender, path);
                    }
                }
            }

            return new CommandMove(World, sender, null);
        }
    }
}
