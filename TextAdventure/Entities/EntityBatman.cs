using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Behaviors;
using TextAdventure.World;

namespace TextAdventure.Entities
{
    public class EntityBatman : EntityNPC
    {
        public EntityBatman(GameWorld world)
            : base(world, 20)
        {
            Name = "Batman";
            Article = Article.None;
            Gender = Gender.Male;
            BaseAttributes.CritChance = .2;
            BaseAttributes.Strength = 12;
            BaseAttributes.Speed = 1;
            BaseAttributes.Accuracy = 30;
            BaseAttributes.Defense = 15;
            BaseAttributes.Dodge = 14;
            Experience = 550;

            Behaviors.Add(new BehaviorRetaliate(this));
            Behaviors.Add(new BehaviorWander(this, 4));
        }
    }
}
