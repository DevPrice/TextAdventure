using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;

namespace TextAdventure.Events
{
    public class AttackMissedEventArgs : EventArgs
    {
        public Entity Entity { get; private set; }

        public AttackMissedEventArgs(Entity entity)
        {
            Entity = entity;
        }
    }
}
