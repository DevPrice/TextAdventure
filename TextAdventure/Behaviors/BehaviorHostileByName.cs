using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;
using TextAdventure.Utility;

namespace TextAdventure.Behaviors
{
    public class BehaviorHostileByName : Behavior
    {
        public string EnemyName { get; private set; }
        public Entity Target { get; private set; }
        public override bool ShouldUpdate
        {
            get
            {
                return Entity.Alive && Entity.Location.Entities.GetByName(EnemyName) != null;
            }
        }

        public BehaviorHostileByName(Entity entity, string enemyName)
            : base(entity)
        {
            EnemyName = enemyName;
            Mask = (int)BehaviorMask.Combat;
        }

        public override void Start()
        {
            base.Start();

            // TODO: This won't work if the entity is hostile to its own kind, fix that.
            Entity.CombatTarget = Target = Entity.Location.Entities.GetByName(EnemyName);
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
