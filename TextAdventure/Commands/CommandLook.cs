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
                Output.WriteLine();
                
                foreach (Item item in ((IMapNode)Target).Items)
                {
                    Output.WriteLine("{0} ", item.Name.ToUpper());
                }

                Output.WriteLine();
                foreach (Path path in World.Map.GetPathsFrom((IMapNode)Target))
                {
                    path.Examine();
                }
            }
        }
        
        public override ICommand Create(ICommandSender sender, string[] args)
        {
            if (sender is Player)
                return new CommandLook(World, sender, World.Map.LocationOf((Player)sender));

            throw new UsageException();
        }
    }
}
