using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;
using TextAdventure.World;

namespace TextAdventure.Events
{
    public class EntityMovedEventArgs : EventArgs
    {
        public readonly Entity Entity;
        public readonly Path Path;

        public EntityMovedEventArgs(Entity entity, Path path)
        {
            Entity = entity;
            Path = path;
        }
    }
}
