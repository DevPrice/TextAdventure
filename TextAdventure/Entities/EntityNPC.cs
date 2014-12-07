using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Behaviors;
using TextAdventure.World;

namespace TextAdventure.Entities
{
    public class EntityNPC : Entity
    {
        public List<Behavior> Behaviors { get; private set; }
        public int BehaviorMask { get; private set; }

        public EntityNPC(GameWorld world)
            : base(world)
        {
            Behaviors = new List<Behavior>();
        }

        public EntityNPC(GameWorld world, int hp)
            : base(world, hp)
        {
            Behaviors = new List<Behavior>();
        }

        public override void Update(TimeSpan delta)
        {
            base.Update(delta);

            BehaviorMask = 0;

            foreach (Behavior behavior in Behaviors)
            {
                if (behavior.ShouldUpdate && (BehaviorMask & behavior.Mask) == 0)
                {
                    if (!behavior.Active)
                        behavior.Start();

                    behavior.Update(delta);

                    BehaviorMask |= behavior.Mask;
                }
                else
                {
                    if (behavior.Active)
                        behavior.Stop();
                }
            }
        }
    }
}
