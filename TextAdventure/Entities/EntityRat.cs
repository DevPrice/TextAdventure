using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Behaviors;
using TextAdventure.World;

namespace TextAdventure.Entities
{
    public class EntityRat : EntityNPC
    {
        public EntityRat(GameWorld world)
            : base(world, 6)
        {
            Name = "rat";
            Behaviors.Add(new BehaviorRetaliate(this, world));
        }
    }
}
