using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;
using TextAdventure.Events;

namespace TextAdventure.Items
{
    public class Equipment : ICollection<ItemWieldable>
    {
        public CombatAttributes TotalAttributes
        {
            get
            {
                CombatAttributes totalAttributes = CombatAttributes.Zero;

                foreach (var item in this)
                {
                    totalAttributes += item.BonusAttributes;
                }

                return totalAttributes;
            }
        }

        #region events

        public event EventHandler<ItemEquipEventArgs> ItemEquipped;
        public event EventHandler<ItemEquipEventArgs> ItemUnequipped;

        #endregion

        private ItemWieldable[] Items;

        public int Count
        {
            get
            {
                int count = 0;

                foreach (var item in this)
                {
                    count++;
                }

                return count;
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public Equipment()
        {
            Items = new ItemWieldable[32];
        }

        public void Add(ItemWieldable item)
        {
            if (!CanEquip(item))
                return;

            int slot = (int)item.Slot;

            for (int i = 0; i < Items.Length; i++)
            {
                // if bit i is set
                if ((slot & 1) == 1)
                {
                    Items[i] = item;
                }

                // check next bit
                slot >>= 1;
            }

            if (ItemEquipped != null)
                ItemEquipped(this, new ItemEquipEventArgs(item));
        }

        public bool Remove(ItemWieldable item)
        {
            if (item == null || !Items.Contains(item))
                return false;

            for (int i = 0; i < Items.Length; i++)
            {
                if (item.Equals(Items[i]))
                    Items[i] = null;
            }

            if (ItemUnequipped != null)
                ItemUnequipped(this, new ItemEquipEventArgs(item));

            return true;
        }

        public ItemWieldable Get(EquipmentSlot slot)
        {
            return Items[GetIndexFromSlot(slot)];
        }

        private static int GetIndexFromSlot(EquipmentSlot slot)
        {
            int intSlot = (int)slot;
            int index = 0;

            while (intSlot > 0)
            {
                intSlot >>= 1;
                index++;
            }

            return index - 1;
        }

        public bool CanEquip(ItemWieldable item)
        {
            int slot = (int)item.Slot;

            for (int i = 0; i < Items.Length; i++)
            {
                // if bit i is set and item already in slot
                if ((slot & 1) == 1 && Items[i] != null)
                {
                    return false;
                }

                // check next bit
                slot >>= 1;
            }

            return true;
        }

        public void Clear()
        {
            for (int i = 0; i < Items.Length; i++)
            {
                Remove(Items[i]);
            }
        }

        public bool Contains(ItemWieldable item)
        {
            return Items.Contains(item);
        }

        public void CopyTo(ItemWieldable[] array, int arrayIndex)
        {
            Array.Copy(Items, arrayIndex, array, 0, Items.Length);
        }

        public IEnumerator<ItemWieldable> GetEnumerator()
        {
            ICollection<ItemWieldable> set = new List<ItemWieldable>();
            
            foreach (ItemWieldable item in Items)
            {
                if (item != null)
                    set.Add(item);
            }

            return set.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public enum EquipmentSlot { Head = 1, Chest = 2, Legs = 4, Feet = 8, RightHand = 16, LeftHand = 32, Neck = 64, Waist = 128, Wrist = 256 }
}
