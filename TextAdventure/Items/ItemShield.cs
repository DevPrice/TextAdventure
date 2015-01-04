using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Items
{
    public class ItemShield : ItemWieldable
    {
        public ItemShield()
            : base("shield", EquipmentSlot.LeftHand)
        {
            BonusAttributes.Defense = 2;
        }
    }
}
