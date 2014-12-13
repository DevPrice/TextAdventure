using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;
using TextAdventure.World;

namespace TextAdventure.Behaviors
{
    public class BehaviorFollowEntity : Behavior
    {
        /// <summary>
        /// Movement speed in movements per minute.
        /// </summary>
        public double MovementSpeed { get; private set; }
        public Entity Target { get; private set; }
        public TimeSpan TimeSinceMoved { get; private set; }

        public override bool ShouldUpdate
        {
            get
            {
                return Entity.Alive && Target != null;
            }
        }

        public BehaviorFollowEntity(Entity entity, Entity follow, double moveSpeed)
            : base(entity)
        {
            MovementSpeed = moveSpeed;
            Target = follow;
            TimeSinceMoved = TimeSpan.Zero;
            Mask = (int)BehaviorMask.Movement;
        }

        public override void Update(TimeSpan delta)
        {
            base.Update(delta);

            TimeSinceMoved += delta;

            if (TimeSinceMoved > TimeSpan.FromMinutes(1 / MovementSpeed))
            {
                List<Path> paths = Entity.World.Map.FindPath(Entity.Location, Target.Location);

                if (paths.Count > 0)
                {
                    Entity.Location.Remove(Entity);
                    paths[0].To.Add(Entity);
                    TimeSinceMoved = TimeSpan.Zero;
                }
            }
        }
    }
}
