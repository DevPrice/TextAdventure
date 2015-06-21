using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Items
{
    public class ItemBattleStaff : ItemWieldable
    {
        public ItemBattleStaff()
            : base("battle staff", EquipmentSlot.LeftHand | EquipmentSlot.RightHand)
        {
            BonusAttributes.Strength = 1;
            BonusAttributes.Speed = .6;
            BonusAttributes.Dodge = 4;
        }
    }
}
