using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;

namespace TextAdventure.Events
{
    public class AttackedEntityEventArgs : EventArgs
    {
        public Entity AttackedEntity { get; private set; }
        public DamageSource DamageSource { get; private set; }
        public double DamageDealt { get; private set; }

        public AttackedEntityEventArgs(Entity entity, DamageSource damageSource, double damageDealt)
        {
            AttackedEntity = entity;
            DamageSource = damageSource;
            DamageDealt = damageDealt;
        }
    }
}
