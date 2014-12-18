using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Entities
{
    public static class Experience
    {
        public static readonly int[] NeededForLevel = { 0, 50, 100, 200, 300, 400, 500, 750, 1000, 1300, 1600, 2000, 2500, 3200, 4200, 5500, 7000, 9001, 12000, 15000, 20000 };

        public static int GetLevelFromExp(int exp)
        {
            int level = 0;

            while (NeededForLevel[level] <= exp)
            {
                exp -= NeededForLevel[level];
                level++;
            }

            return level;
        }
    }
}
