using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entity;
using TextAdventure.Utility;

namespace TextAdventure.Command
{
    public class CommandStatus : Command
    {
        public CommandStatus()
        {
            Name = "status";
            Aliases.Add("hp");
        }

        public override void Execute(ICommandSender sender)
        {
            if (sender is Player)
                Output.WriteLine("HP: {0}", ((Player)sender).Hp);
        }

        public override ICommand Create(string[] args)
        {
            return new CommandStatus();
        }
    }
}
