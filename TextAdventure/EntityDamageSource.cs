using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;

namespace TextAdventure
{
    public class EntityDamageSource : DamageSource
    {
        public Entity Source { get; private set; }
        public EntityDamageSource(Entity entity)
        {
            Source = entity;
        }

        public override string ToString()
        {
            return Source.Name;
        }
    }
}
