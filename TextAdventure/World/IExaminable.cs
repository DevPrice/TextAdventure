using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Commands;
using TextAdventure.Sense;

namespace TextAdventure.World
{
    public interface IExaminable : IObservable
    {
        void Examine(ICommandSender examiner);
    }
}
