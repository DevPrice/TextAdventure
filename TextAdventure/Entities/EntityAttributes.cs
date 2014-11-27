using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Entities
{
    public class EntityAttributes
    {
        public float MaxHp;
        public float Strength;
        public float Defense;
        public float Accuracy;
        public float Dodge;
        public float CritChance;
        public float CritMultiplier;

        public EntityAttributes()
        {
            MaxHp = 1;
            Strength = 1;
            Defense = 1;
            Accuracy = 1;
            Dodge = 1;
            CritChance = 0;
            CritMultiplier = 2;
        }

        public EntityAttributes(float maxHp, float strength, float defense, float accuracy, float dodge, float critChance, float critMultiplier)
        {
            MaxHp = maxHp;
            Strength = strength;
            Defense = defense;
            Accuracy = accuracy;
            Dodge = dodge;
            CritChance = critChance;
            CritMultiplier = critMultiplier;
        }

        public static EntityAttributes Combine(EntityAttributes a1, EntityAttributes a2)
        {
            return new EntityAttributes(a1.MaxHp + a2.MaxHp,
                a1.Strength + a2.Strength, a1.Defense + a2.Defense,
                a1.Accuracy + a2.Accuracy, a1.Dodge + a2.Dodge,
                a1.CritChance + a2.CritChance, a1.CritMultiplier + a2.CritMultiplier);
        }
    }
}
