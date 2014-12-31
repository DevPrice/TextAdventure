using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Entities
{
    public class Race : INamed
    {
        public static IReadOnlyCollection<Race> All { get { return new List<Race>() { Human, Elf, Dwarf }.AsReadOnly();  } }
        public static readonly Race Human;
        public static readonly Race Elf;
        public static readonly Race Dwarf;

        static Race()
        {
            Human = new Race("Human", CombatAttributes.Default, new CombatAttributes(2.5, 2, 2, 2, 1, .04, 0, 0));
            Elf = new Race("Elf", CombatAttributes.Default, new CombatAttributes(2.5, 1, 1, 3, 2, .06, 0, 0));
            Dwarf = new Race("Dwarf", CombatAttributes.Default, new CombatAttributes(2, 3, 4, 2, 1, .01, 0, 0));
        }

        public static Race GetRandom(Random random)
        {
            IReadOnlyCollection<Race> all = All;
            int numRaces = all.Count;

            return all.ElementAt(random.Next(0, numRaces));
        }

        public string Name { get; private set; }
        public readonly CombatAttributes BaseAttributes;
        public readonly CombatAttributes AttributesPerLevel;

        public Race(string name, CombatAttributes baseAttributes, CombatAttributes perLevel)
        {
            Name = name;
            BaseAttributes = baseAttributes;
            AttributesPerLevel = perLevel;
        }
    }
}
