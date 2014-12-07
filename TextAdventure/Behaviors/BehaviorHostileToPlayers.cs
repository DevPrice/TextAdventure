using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;
using TextAdventure.Utility;

namespace TextAdventure.Behaviors
{
    public class BehaviorHostileToPlayers : Behavior
    {
        public Entity Target { get; private set; }
        public override bool ShouldUpdate
        {
            get
            {
                if (!Entity.Alive)
                    return false;

                foreach (Entity entity in Entity.Location.Entities)
                {
                    if (entity is Player)
                        return true;
                }

                return false;
            }
        }

        public BehaviorHostileToPlayers(Entity entity)
            : base(entity)
        {
            Mask = (int)BehaviorMask.Combat;
        }

        public override void Update(TimeSpan delta)
        {
            base.Update(delta);

            List<Player> players = new List<Player>();

            foreach (Entity entity in Entity.Location.Entities)
            {
                if (entity is Player)
                    players.Add((Player)entity);
            }

            Target = players[Entity.World.Random.Next(players.Count)];
            Entity.CombatTarget = Target;
        }

        public override void Stop()
        {
            base.Stop();

            if (Entity.CombatTarget == Target)
                Entity.CombatTarget = null;
        }
    }
}
