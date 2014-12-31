using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;
using TextAdventure.World;

namespace TextAdventure.Behaviors
{
    public class BehaviorWander : Behavior
    {
        /// <summary>
        /// Movement speed in movements per minute.
        /// </summary>
        public double MovementSpeed { get; private set; }
        public TimeSpan TimeSinceMoved { get; private set; }
        private TimeSpan Variation { get; set; }

        public override bool ShouldUpdate
        {
            get
            {
                return Entity.Alive && Entity.CombatTarget == null;
            }
        }

        public BehaviorWander(Entity entity, double moveSpeed)
            : base(entity)
        {
            MovementSpeed = moveSpeed;
            TimeSinceMoved = TimeSpan.Zero;
            Variation = TimeSpan.FromMilliseconds(Entity.World.Random.NextDouble() * .25 / MovementSpeed);
            Mask = (int)BehaviorMask.Movement;
        }

        public override void Update(TimeSpan delta)
        {
            base.Update(delta);

            TimeSinceMoved += delta;

            if (TimeSinceMoved > TimeSpan.FromMinutes(1 / MovementSpeed) + Variation)
            {
                List<Path> paths = Entity.World.Map.GetPathsFrom(Entity.Location);

                if (paths.Count > 0)
                {
                    Entity.UsePath(paths[Entity.World.Random.Next(paths.Count)]);

                    TimeSinceMoved = TimeSpan.Zero;
                    Variation = TimeSpan.FromMilliseconds(Entity.World.Random.NextDouble() * .25 / MovementSpeed);
                }
            }
        }
    }
}
