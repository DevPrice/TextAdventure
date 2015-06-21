using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Commands;
using TextAdventure.Entities;
using TextAdventure.Utility;
using TextAdventure.World;

namespace TextAdventure.Items
{
    public class Item : IExaminable, IUpdatable, INoun
    {

        public Gender Gender { get; set; }

        public Article Article { get; set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        public Item(string name)
        {
            Name = name;
            Description = RantEngine.RunPattern(String.Format("\\a {0}.", Name)).ToTitleCase();
        }

        public void Examine(ICommandSender examiner)
        {
            examiner.SendLine(Description);
        }

        public virtual void Update(TimeSpan delta)
        {

        }

        public virtual void OnPickup(Entity entity)
        {

        }
    }
}
