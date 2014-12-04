using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Items
{
    public class ItemSword : ItemWieldable
    {
        public ItemSword()
            : base("sword", EquipmentSlot.RightHand)
        {
            BonusAttributes.Strength = 3;
        }
    }
}
