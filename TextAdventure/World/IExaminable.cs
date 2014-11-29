using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Commands;

namespace TextAdventure.World
{
    public interface IExaminable
    {
        void Examine(ICommandSender examiner);
    }
}
