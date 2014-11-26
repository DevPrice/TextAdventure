using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.World;

namespace TextAdventure.Command
{
    public interface ICommand
    {
        string Name { get; }
        List<string> Aliases { get; }

        void Execute();
    }
}
