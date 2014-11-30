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
        public Entity Entity { get; private set; }
        public IMapNode MovedFrom { get; private set; }
        public IMapNode MovedTo { get; private set; }

        public EntityMovedEventArgs(Entity entity, IMapNode from, IMapNode to)
        {
            Entity = entity;
            MovedFrom = from;
            MovedTo = to;
        }
    }
}
