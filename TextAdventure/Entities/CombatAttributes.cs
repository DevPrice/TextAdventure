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
        public static CombatAttributes Default { get { return new CombatAttributes(10, 2, 2, 10, 10, .5, 0, 2); } }

        public double MaxHp;
        public double Strength;
        public double Defense;
        public double Accuracy;
        public double Dodge;
        public double Speed;
        public double CritChance;
        public double CritMultiplier;

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

        public static CombatAttributes Add(CombatAttributes a1, CombatAttributes a2)
        {
            return new CombatAttributes(a1.MaxHp + a2.MaxHp,
                a1.Strength + a2.Strength, a1.Defense + a2.Defense,
                a1.Accuracy + a2.Accuracy, a1.Dodge + a2.Dodge, a1.Speed + a1.Speed,
                a1.CritChance + a2.CritChance, a1.CritMultiplier + a2.CritMultiplier);
        }

        public static CombatAttributes operator +(CombatAttributes a1, CombatAttributes a2)
        {
            return Add(a1, a2);
        }
    }

    public static class PerLevel
    {
        public static readonly CombatAttributes Human = new CombatAttributes(2.5, 2, 2, 2, 1, .04, 0, 0);
        public static readonly CombatAttributes Elf =   new CombatAttributes(2.5, 1, 1, 3, 2, .06, 0, 0);
    }
}
