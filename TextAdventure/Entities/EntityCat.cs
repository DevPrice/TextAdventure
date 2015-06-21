using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Behaviors;
using TextAdventure.Utility;
using TextAdventure.World;

namespace TextAdventure.Entities
{
    public class EntityCat : EntityNPC
    {
        private static readonly string[] Meows;

        static EntityCat()
        {
            Meows = new string[] { "Meow.", "Meow!", "Mew.", "Meow...", "Meow?", "Nyan." };
        }

        public EntityCat(GameWorld world)
            : base(world, 6)
        {
            Name = "cat";
            BaseAttributes.CritChance = .2;
            BaseAttributes.Strength = 1;
            BaseAttributes.Speed = .8;
            BaseAttributes.Accuracy = 15;
            Experience = 30;

            Behaviors.Add(new BehaviorRetaliate(this));
            Behaviors.Add(new BehaviorHostileByName(this, "rat"));
            Behaviors.Add(new BehaviorWander(this, 3));
            Behaviors.Add(new BehaviorRant(this, Meows));
        }
    }
}
