using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entity;
using TextAdventure.Utility;

namespace TextAdventure.Command
{
    public class CommandStatus : CommandBase<Player>
    {
        public CommandStatus(Player p)
            : base(p)
        {
            Name = "status";
            Aliases.Add("hp");
        }

        public override void Execute(ICommandSender sender)
        {
            Output.WriteLine(String.Format("HP: {0}", Target.Hp));
        }

        public override ICommand Create(string[] args)
        {
            return new CommandStatus(new Player());
        }
    }
}
