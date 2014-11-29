using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Commands;
using TextAdventure.Items;
using TextAdventure.Utility;
using TextAdventure.World;

namespace TextAdventure.Entities
{
    public abstract class Entity : IExaminable, IUpdatable
    {
        public string Name { get; set; }
        public EntityAttributes Attributes { get; set; }
        public Gender Gender { get; set; }
        private double _Hp;
        public double Hp
        {
            get { return _Hp; }
            set
            {
                _Hp = value.Clamp(0, Attributes.MaxHp);
            }
        }
        public bool Alive { get { return Hp > 0; } }
        public List<Item> Inventory { get; set; }
        public Entity CombatTarget { get; set; }
        public TimeSpan TimeSinceAttack { get; set; }

        public Entity(GameWorld world)
        {
            Attributes = new EntityAttributes();
            Hp = Attributes.MaxHp;
            Inventory = new List<Item>();
            TimeSinceAttack = TimeSpan.FromSeconds(30);
        }

        public Entity(GameWorld world, int hp)
            : this(world)
        {
            Hp = Attributes.MaxHp = hp;
        }

        public virtual void Examine(ICommandSender examiner)
        {

        }

        public virtual void Update(TimeSpan delta)
        {
            TimeSinceAttack += delta;

            if (!CombatTarget.Alive)
                CombatTarget = null;

            if (CombatTarget != null)
            {
                TryCombat(CombatTarget);
            }
        }

        private void TryCombat(Entity target)
        {
            if (!target.Alive)
                return;

            if (TimeSinceAttack > TimeSpan.FromSeconds(1 / Attributes.Speed))
            {
                Attack(target);
            }
        }

        public virtual void Attack(Entity target)
        {
            double chanceToHit = Attributes.Accuracy / (Attributes.Accuracy + target.Attributes.Dodge);

            Random rng = new Random(GetHashCode() + Environment.TickCount);

            bool hit = rng.NextDouble() < chanceToHit;

            if (hit)
            {
                double damage = Attributes.Strength;

                bool crit = rng.NextDouble() < Attributes.CritChance;

                if (crit)
                    damage *= Attributes.CritMultiplier;

                int startHp = (int)Math.Ceiling(target.Hp);
                target.DealDamage(new EntityDamageSource(this), damage);
                int damageDealt = (int)(startHp - Math.Ceiling(target.Hp));

                if (this is ICommandSender)
                {
                    ((ICommandSender)this).SendMessage("You attack {0} for {1} damage{2}", target.Name, damageDealt, crit ? "!" : ".");
                }
            }
            else if (this is ICommandSender)
            {
                ((ICommandSender)this).SendMessage("You miss!");
            }

            TimeSinceAttack = TimeSpan.Zero;
        }

        public virtual double DealDamage(DamageSource source, double amount)
        {
            int startHp = (int)Math.Ceiling(Hp);

            double finalDamage = amount;

            if (source.DamageType == DamageType.Physical)
            {
                finalDamage = finalDamage * 10 / (10 + Attributes.Defense);
            }

            Hp -= finalDamage;

            int damageDealt = (int)(startHp - Math.Ceiling(Hp));

            if (this is ICommandSender)
            {
                ((ICommandSender)this).SendMessage("You take {0} damage.", damageDealt);
            }

            return finalDamage;
        }
    }

    public enum Gender { Neutral, Male, Female }
}
