using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;

namespace TextAdventure.Events
{
    public class LevelUpEventArgs : EventArgs
    {
        public Entity ExperienceSource { get; private set; }
        public int ExperienceAmount { get; private set; }

        public LevelUpEventArgs(Entity source, int amount)
        {
            ExperienceSource = source;
            ExperienceAmount = amount;
        }
    }
}
