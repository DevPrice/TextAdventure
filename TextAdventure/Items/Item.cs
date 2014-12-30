using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Commands;
using TextAdventure.Utility;
using TextAdventure.World;

namespace TextAdventure.Items
{
    public class Item : IExaminable, IUpdatable, INamed

    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        public Item(string name)
        {
            Name = name;
            Description = String.Format("A {0}.", Name);
        }

        public void Examine(ICommandSender examiner)
        {
            examiner.SendLine(Description);
        }

        public virtual void Update(TimeSpan delta)
        {

        }
    }
}
