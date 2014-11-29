using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Behaviors;

namespace TextAdventure.Entities
{
    public class EntityNPC : Entity
    {
        public List<Behavior> Behaviors { get; set; }

        public override void Update(TimeSpan delta)
        {
            base.Update(delta);

            foreach (Behavior behavior in Behaviors)
            {
                if (behavior.ShouldUpdate)
                {
                    if (!behavior.Active)
                        behavior.Start();

                    behavior.Update(delta);
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
