using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;
using TextAdventure.Utility;

namespace TextAdventure.Behaviors
{
    public class BehaviorRant : Behavior
    {
        private IList<string> Rants;

        public override bool ShouldUpdate
        {
            get
            {
                return Entity.Alive;
            }
            protected set
            {
                base.ShouldUpdate = value;
            }
        }

        public BehaviorRant(Entity entity, IList<string> rants)
            : base(entity)
        {
            Rants = rants;
        }

        public override void Update(TimeSpan delta)
        {
            base.Update(delta);

            if (Entity.World.Random.Next(750) == 0)
            {
                Entity.Location.Broadcast(String.Format("{0} says, \"{1}\"", Entity.Name.ToTitleCase(), Rants[Entity.World.Random.Next(Rants.Count)]));
            }
        }
    }
}
