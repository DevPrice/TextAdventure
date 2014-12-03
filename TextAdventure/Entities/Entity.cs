using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TextAdventure.Commands;
using TextAdventure.Events;
using TextAdventure.Items;
using TextAdventure.Utility;
using TextAdventure.World;

namespace TextAdventure.Entities
{
    public abstract class Entity : IExaminable, IUpdatable
    {
        public GameWorld World { get; private set; }
        public string Name { get; set; }
        public CombatAttributes Attributes { get; set; }
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
        public Equipment Equipment { get; set; }
        public Entity CombatTarget { get; set; }
        public TimeSpan TimeSinceAttack { get; set; }
        public int Experience { get; protected set; }

        #region events

        public event EventHandler<DamageTakenEventArgs> DamageTaken;
        public event EventHandler<DamageTakenEventArgs> Death;
        public event EventHandler<AttackedEntityEventArgs> AttackedEntity;
        public event EventHandler<AttackedEntityEventArgs> KilledEntity;

        #endregion

        public Entity(GameWorld world)
        {
            World = world;
            Attributes = new CombatAttributes();
            Hp = Attributes.MaxHp;
            Inventory = new List<Item>();
            Equipment = new Equipment();
            TimeSinceAttack = TimeSpan.FromSeconds(30);
        }


        public Entity(GameWorld world, int hp)
            : this(world)
        {
            Hp = Attributes.MaxHp = hp;
        }

        public virtual void Examine(ICommandSender examiner)
        {
            examiner.SendMessage(Name.ToTitleCase());

            if (!Alive)
            {
                examiner.SendMessage();
                examiner.SendMessage("It's dead.");
            }
            else if (Hp < Attributes.MaxHp / 2)
            {
                examiner.SendMessage();
                examiner.SendMessage("It looks injured.");
            }
        }

        public virtual void Update(TimeSpan delta)
        {
            TimeSinceAttack += delta;

            if (CombatTarget != null && !CombatTarget.Alive)
                CombatTarget = null;

            if (CombatTarget != null)
            {
                TryCombat(CombatTarget);
            }
        }

        private void TryCombat(Entity target)
        {
            if (!Alive || !target.Alive)
                return;

            if (World.Map.LocationOf(this) != World.Map.LocationOf(target))
                return;

            if (TimeSinceAttack > TimeSpan.FromSeconds(1 / Attributes.Speed))
            {
                AttackEntity(target);
            }
        }

        public virtual void AttackEntity(Entity target)
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

                DamageSource damageSource = new EntityDamageSource(this);

                target.DealDamage(damageSource, damage);

                if (AttackedEntity != null)
                    AttackedEntity(this, new AttackedEntityEventArgs(target, damageSource, damage));

                if (!target.Alive && AttackedEntity != null)
                    KilledEntity(this, new AttackedEntityEventArgs(target, damageSource, damage));
            }
            else if (this is ICommandSender)
            {
                ((ICommandSender)this).SendMessage("You miss!");
            }

            TimeSinceAttack = TimeSpan.Zero;
        }

        public virtual double DealDamage(DamageSource source, double amount)
        {
            if (source.DamageType == DamageType.Physical)
            {
                amount *= 10 / (10 + Attributes.Defense);
            }

            Hp -= amount;

            if (DamageTaken != null)
                DamageTaken(this, new DamageTakenEventArgs(source, amount));

            if (!Alive && Death != null)
                Death(this, new DamageTakenEventArgs(source, amount));

            return amount;
        }
    }

    public enum Gender { Neutral, Male, Female }
}
