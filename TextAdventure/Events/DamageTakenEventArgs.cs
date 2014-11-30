using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Events
{
    public class DamageTakenEventArgs : EventArgs
    {
        public DamageSource DamageSource;
        public double Amount;

        public DamageTakenEventArgs(DamageSource source, double amount)
        {
            DamageSource = source;
            Amount = amount;
        }
    }
}
