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
            : base(world, 4)
        {
            Name = "rat";
            BaseAttributes.Strength = 1;
            BaseAttributes.Defense = 0;
            BaseAttributes.Dodge = 5;
            BaseAttributes.Speed = .4;
            Experience = 10;
            
            Behaviors.Add(new BehaviorRetaliate(this));
        }
    }
}
