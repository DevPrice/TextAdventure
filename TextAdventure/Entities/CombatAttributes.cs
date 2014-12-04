using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Entities
{
    public class CombatAttributes
    {
        public static CombatAttributes Zero { get { return new CombatAttributes(0, 0, 0, 0, 0, 0, 0, 0); } }

        public double MaxHp;
        public double Strength;
        public double Defense;
        public double Accuracy;
        public double Dodge;
        public double Speed;
        public double CritChance;
        public double CritMultiplier;
        public CombatAttributes()
        {
            MaxHp = 1;
            Strength = 1;
            Defense = 1;
            Accuracy = 1;
            Dodge = 1;
            Speed = .5f;
            CritChance = 0;
            CritMultiplier = 2;
        }

        public CombatAttributes(double maxHp, double strength, double defense, double accuracy,
            double dodge, double speed, double critChance, double critMultiplier)
        {
            MaxHp = maxHp;
            Strength = strength;
            Defense = defense;
            Accuracy = accuracy;
            Dodge = dodge;
            Speed = speed;
            CritChance = critChance;
            CritMultiplier = critMultiplier;
        }

        public static CombatAttributes Combine(CombatAttributes a1, CombatAttributes a2)
        {
            return new CombatAttributes(a1.MaxHp + a2.MaxHp,
                a1.Strength + a2.Strength, a1.Defense + a2.Defense,
                a1.Accuracy + a2.Accuracy, a1.Dodge + a2.Dodge, a1.Speed + a1.Speed,
                a1.CritChance + a2.CritChance, a1.CritMultiplier + a2.CritMultiplier);
        }
    }
}
