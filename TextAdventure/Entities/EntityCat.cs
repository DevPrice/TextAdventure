using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Behaviors;
using TextAdventure.World;

namespace TextAdventure.Entities
{
    public class EntityCat : EntityNPC
    {
        public EntityCat(GameWorld world)
            : base(world, 8)
        {
            Name = "cat";
            BaseAttributes.CritChance = .2;
            BaseAttributes.Strength = 1;
            BaseAttributes.Speed = .8;
            BaseAttributes.Accuracy = 15;

            Behaviors.Add(new BehaviorRetaliate(this));
            Behaviors.Add(new BehaviorHostileByName(this, "rat"));
            Behaviors.Add(new BehaviorWander(this, 3));
        }
    }
}
