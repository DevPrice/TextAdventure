using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;

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
        }
    }
}
