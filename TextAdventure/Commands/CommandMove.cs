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
        private static List<string> CardinalDirections = new List<string> { "north", "east", "south", "west" };

        public CommandMove(GameWorld world, ICommandSender sender, Path path)
            : base(world, sender, path)
        {
            Name = "move";
            Aliases.Add("go");
            Aliases.Add("goto");
            Aliases.Add("n");
            Aliases.Add("s");
            Aliases.Add("e");
            Aliases.Add("w");

            foreach (var direction in CardinalDirections)
                Aliases.Add(direction);

            Usage = "move [pathname]";
        }

        public override void Execute()
        {
            base.Execute();

            if (Target != null)
            {
                ((Player)Sender).UsePath(Target);

                Sender.SendLine("You go {0}.", Target.Identifier);
            }
            else
            {
                Sender.SendLine("You can't go that way.");
            }
        }

        public override ICommand Create(ICommandSender sender, string[] args)
        {
            if (!(sender is Player))
                throw new UsageException();

            string chosenPath = args[0];

            if (args.Length >= 2)
            {
                chosenPath = args[1];
            }

            if (chosenPath == "n")
                chosenPath = "north";

            if (chosenPath == "s")
                chosenPath = "south";

            if (chosenPath == "e")
                chosenPath = "east";

            if (chosenPath == "w")
                chosenPath = "west";

            IMapNode currentNode = World.Map.LocationOf((Player)sender);

            foreach (Path path in World.Map.GetPathsFrom(currentNode))
            {
                if (path.Identifier.Equals(chosenPath, StringComparison.CurrentCultureIgnoreCase))
                {
                    return new CommandMove(World, sender, path);
                }
            }

            return new CommandMove(World, sender, null);
        }
    }
}
