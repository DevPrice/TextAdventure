using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Behaviors;
using TextAdventure.Events;
using TextAdventure.Utility;
using TextAdventure.World;

namespace TextAdventure.Entities
{
    public class EntityNPC : Entity
    {
        public List<Behavior> Behaviors { get; private set; }
        public int BehaviorMask { get; private set; }

        public EntityNPC(GameWorld world)
            : this(world, (int)CombatAttributes.Default.MaxHp)
        {

        }

        public EntityNPC(GameWorld world, int hp)
            : base(world, hp)
        {
            Behaviors = new List<Behavior>();

            AttackedEntity += Entity_AttackedEntity;
            Death += Entity_Death;
        }

        private void Entity_AttackedEntity(object sender, AttackedEntityEventArgs e)
        {
            foreach (Entity entity in Location.Entities)
            {
                if (entity is Player && entity != e.AttackedEntity)
                    ((Player)entity).SendLine("{0} attacks {1}.", ((Entity)sender).Name.ToTitleCase(), e.AttackedEntity.Name);
            }
        }

        private void Entity_Death(object sender, DamageTakenEventArgs e)
        {
            foreach (Entity entity in Location.Entities)
            {
                if (entity is Player)
                {
                    if (!(e.DamageSource is EntityDamageSource) || ((EntityDamageSource)e.DamageSource).Source != entity)
                        ((Player)entity).SendLine("{0} was killed by {1}.", ((Entity)sender).Name.ToTitleCase(), e.DamageSource.ToString());
                }
            }
        }

        public override void Update(TimeSpan delta)
        {
            base.Update(delta);

            BehaviorMask = 0;

            // Stop behaviors that should no longer update
            foreach (Behavior behavior in Behaviors)
            {
                if (behavior.ShouldUpdate && (BehaviorMask & behavior.Mask) == 0)
                {
                    BehaviorMask |= behavior.Mask;
                }
                else
                {
                    if (behavior.Active)
                        behavior.Stop();
                }
            }

            BehaviorMask = 0;

            // Update behaviors and start as necessary
            foreach (Behavior behavior in Behaviors)
            {
                if (behavior.ShouldUpdate && (BehaviorMask & behavior.Mask) == 0)
                {
                    if (!behavior.Active)
                        behavior.Start();

                    behavior.Update(delta);

                    BehaviorMask |= behavior.Mask;
                }
            }
        }
    }
}
