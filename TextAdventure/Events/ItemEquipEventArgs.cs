﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;
using TextAdventure.Items;

namespace TextAdventure.Events
{
    public class ItemEquipEventArgs : EventArgs
    {
        public ItemWieldable Item { get; private set; }

        public ItemEquipEventArgs(ItemWieldable item)
        {
            Item = item;
        }
    }
}
