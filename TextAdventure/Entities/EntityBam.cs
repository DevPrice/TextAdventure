using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Behaviors;
using TextAdventure.Items;
using TextAdventure.World;

namespace TextAdventure.Entities
{
    public class EntityBam : EntityNPC
    {
        public EntityBam(GameWorld world)
            : base(world, 16)
        {
            Name = "Bam";
            Article = Article.None;
            Gender = Gender.Male;

            BaseAttributes.Strength = 3;
            BaseAttributes.Speed = 1.1;
            BaseAttributes.Dodge = 14;

            Behaviors.Add(new BehaviorRetaliate(this));
            Behaviors.Add(new BehaviorWander(this, 1.4));
            Behaviors.Add(new BehaviorRant(this, new List<string> { "Is this your card?", "Bam!", "This staff is dimensional!" }));
            var weapon = new ItemBattleStaff();
            Inventory.Add(weapon);
            Equipment.Add(weapon);
        }
    }
}
