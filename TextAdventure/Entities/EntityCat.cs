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
            Meows = new string[] { "Meow!", "Mew.", "Meow...", "Meow?", "Nyan." };
        }

        public EntityCat(GameWorld world)
            : base(world, 6)
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

        public override void Update(TimeSpan delta)
        {
            base.Update(delta);

            if (World.Random.Next(750) == 0)
            {
                Location.Broadcast(String.Format("{0} says, \"{1}\"", Name.ToTitleCase(), Meows[World.Random.Next(Meows.Length)]));
            }
        }
    }
}
