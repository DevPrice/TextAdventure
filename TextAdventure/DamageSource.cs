using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class DamageSource
    {
        public static readonly DamageSource World;

        public DamageType DamageType { get; protected set; }

        static DamageSource()
        {
            World = new DamageSource { DamageType = DamageType.True };
        }

        public DamageSource()
        {
            DamageType = DamageType.Physical;
        }
    }

    public enum DamageType { Physical, Magical, True }
}
