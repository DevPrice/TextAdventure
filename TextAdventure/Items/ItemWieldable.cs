using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;

namespace TextAdventure.Items
{
    public class ItemWieldable : Item
    {
        public CombatAttributes BonusAttributes { get; protected set; }
        public EquipmentSlot Slot { get; protected set; }

        public ItemWieldable(string name)
            : this(name, EquipmentSlot.RightHand)
        {

        }

        public ItemWieldable(string name, EquipmentSlot slot)
            : this(name, slot, CombatAttributes.Zero)
        {

        }

        public ItemWieldable(string name, EquipmentSlot slot, CombatAttributes attributes)
            : base(name)
        {
            BonusAttributes = attributes;
            Slot = slot;
        }
    }
}
