using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.World;

namespace TextAdventure.Commands
{
    public class CommandLook : Command<IExaminable>
    {
        public CommandLook(GameWorld world, IExaminable lookAt)
            : base(world, lookAt)
        {
            Name = "look";
            Aliases.Add("examine");
            Aliases.Add("view");
        }
        
        public override ICommand Create(string[] args)
        {
            return new CommandLook(World, null);
        }
    }
}
