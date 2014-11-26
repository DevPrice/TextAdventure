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
            
        }

        public override void Execute()
        {
            Output.WriteLine(String.Format("HP: {0}", Target.Hp));
        }
    }
}
