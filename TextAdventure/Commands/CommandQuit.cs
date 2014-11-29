using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.World;

namespace TextAdventure.Commands
{
    public class CommandQuit : Command<GameServer>
    {
        public CommandQuit(GameWorld world, ICommandSender sender, GameServer server)
            : base(world, sender, server)
        {
            Name = "quit";
            Aliases.Add("exit");
            RequiredPermission = CommandPermission.Admin;
        }

        public override void Execute()
        {
            base.Execute();

            Target.Stop();
        }

        public override ICommand Create(ICommandSender sender, string[] args)
        {
            return new CommandQuit(World, sender, Target);
        }
    }
}
