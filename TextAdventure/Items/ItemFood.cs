using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;
using TextAdventure.Utility;

namespace TextAdventure.Items
{
    public class ItemFood : Item, IEdible
    {
        public double HealAmount { get; protected set; }

        public ItemFood(string name)
            : this(name, 0)
        {

        }

        public ItemFood(string name, double healAmount)
            : base(name)
        {
            HealAmount = healAmount;
        }

        public virtual void Eat(Entity eater)
        {
            eater.Hp += HealAmount;

            // TODO: Refactor this.
            if (eater is Player)
            {
                ((Player)eater).SendLine("You eat {0}.", this.GetFullName());

                if (HealAmount > 0)
                    ((Player)eater).SendLine("{0} health restored.", Math.Round(HealAmount));
            }
        }
    }
}
