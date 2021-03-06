﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;
using TextAdventure.Events;
using TextAdventure.World;

namespace TextAdventure.Behaviors
{
    public class BehaviorRetaliate : Behavior
    {
        public override bool ShouldUpdate { get; protected set; }
        public Entity Target { get; private set; }

        public BehaviorRetaliate(Entity entity)
            : base(entity)
        {
            Entity.DamageTaken += Entity_DamageTaken;
            Mask = (int)BehaviorMask.Combat;
        }

        private void Entity_DamageTaken(object sender, DamageTakenEventArgs e)
        {
            if (e.DamageSource is EntityDamageSource)
            {
                EntityDamageSource entitySource = (EntityDamageSource)e.DamageSource;
                Target = entitySource.Source;
                ShouldUpdate = true;
            }
        }

        public override void Start()
        {
            base.Start();

            Entity.CombatTarget = Target;
        }

        public override void Update(TimeSpan delta)
        {
            base.Update(delta);

            if (Target != null && !Target.Alive)
            {
                ShouldUpdate = false;
            }
        }

        public override void Stop()
        {
            base.Stop();

            if (Entity.CombatTarget == Target)
                Entity.CombatTarget = null;
        }
    }
}
