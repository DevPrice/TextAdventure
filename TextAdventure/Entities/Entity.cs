﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TextAdventure.Commands;
using TextAdventure.Events;
using TextAdventure.Items;
using TextAdventure.Sense;
using TextAdventure.Utility;
using TextAdventure.World;

namespace TextAdventure.Entities
{
    public abstract class Entity : IExaminable, IUpdatable, INoun, IObserver
    {
        public GameWorld World { get; private set; }
        public IMapNode Location { get { return World.Map.LocationOf(this); } }
        public Article Article { get; protected set; }
        public string Name { get; set; }
        public string Pronoun
        {
            get
            {
                return RantEngine.RunPattern(String.Format("<pron.nom-{0}>", Gender.GetRantPattern()));
            }
        }
        public CombatAttributes Attributes { get { return BaseAttributes + Equipment.TotalAttributes; } }
        public CombatAttributes BaseAttributes { get; set; }
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
        public List<IObserver> Observers;

        #region events

        public event EventHandler<DamageTakenEventArgs> DamageTaken;
        public event EventHandler<DamageTakenEventArgs> Death;
        public event EventHandler<AttackedEntityEventArgs> AttackedEntity;
        public event EventHandler<AttackMissedEventArgs> AttackMissed;
        public event EventHandler<AttackedEntityEventArgs> KilledEntity;
        public event EventHandler<EntityMovedEventArgs> Moved;

        #endregion

        public Entity(GameWorld world)
        {
            World = world;
            Article = Article.Indefinite;
            BaseAttributes = CombatAttributes.Default;
            Equipment = new Equipment();
            Inventory = new List<Item>();
            TimeSinceAttack = TimeSpan.FromSeconds(30);
            Hp = BaseAttributes.MaxHp;
            Observers = new List<IObserver>();
        }

        public Entity(GameWorld world, int hp)
            : this(world)
        {
            Hp = BaseAttributes.MaxHp = hp;
        }

        public virtual void Examine(ICommandSender examiner)
        {
            ExamineBasic(examiner);

            ExamineInjury(examiner);
        }

        protected void ExamineInjury(ICommandSender examiner)
        {
            if (!Alive)
            {
                examiner.SendLine();
                examiner.SendLine("{0}'s dead.", Pronoun.FirstCharToUpper());
            }
            else if (Hp < Attributes.MaxHp / 2)
            {
                examiner.SendLine();
                examiner.SendLine("{0} looks injured.", Pronoun.FirstCharToUpper());
            }
        }

        protected void ExamineBasic(ICommandSender examiner)
        {
            examiner.SendLine(this.GetFullName().FirstCharToUpper() + ". " + Gender.ToSymbol());
        }

        public virtual void Update(TimeSpan delta)
        {
            TimeSinceAttack += delta;

            foreach (Item item in Inventory)
            {
                item.Update(delta);
            }

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
            double chanceToHit = 0;

            if (Attributes.Accuracy + target.Attributes.Dodge != 0)
            {
                chanceToHit = Attributes.Accuracy / (Attributes.Accuracy + target.Attributes.Dodge);
            }

            Random rng = new Random(GetHashCode() ^ Environment.TickCount);

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

                if (!target.Alive && AttackedEntity != null && KilledEntity != null)
                    KilledEntity(this, new AttackedEntityEventArgs(target, damageSource, damage));
            }
            else if (AttackMissed != null)
            {
                AttackMissed(this, new AttackMissedEventArgs(target));
            }

            TimeSinceAttack = TimeSpan.Zero;
        }

        public virtual double DealDamage(DamageSource source, double amount)
        {
            bool alreadyDead = !Alive;

            if (source.DamageType == DamageType.Physical)
            {
                amount *= 10 / (10 + Attributes.Defense);
            }

            Hp -= amount;

            if (DamageTaken != null)
                DamageTaken(this, new DamageTakenEventArgs(source, amount));

            if (!Alive && !alreadyDead && Death != null)
                Death(this, new DamageTakenEventArgs(source, amount));
            
            return amount;
        }

        public bool UsePath(string pathId)
        {
            List<Path> paths = World.Map.GetPathsFrom(Location);

            foreach (Path path in paths)
            {
                if (path.Identifier.Equals(pathId, StringComparison.CurrentCultureIgnoreCase))
                    return UsePath(path);
            }

            return false;
        }

        public bool UsePath(Path path)
        {
            World.Map.MoveEntity(this, path);

            if (Moved != null)
                Moved(this, new EntityMovedEventArgs(this, path));

            return true;
        }

        public bool CanObserve(IObservable observerable)
        {
            foreach (IObserver observer in Observers)
            {
                if (observer.CanObserve(observerable))
                    return true;
            }

            return false;
        }

        public void Observe(IObservable observerable)
        {
            foreach (IObserver observer in Observers)
            {
                if (observer.CanObserve(observerable))
                    observer.Observe(observerable);
            }
        }
    }
}
