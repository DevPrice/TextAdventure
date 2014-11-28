using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Utility;
using TextAdventure.World;

namespace TextAdventure.Items
{
    public class Item : IExaminable
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Item(string name)
        {
            Name = name;
            Description = "A " + Name + ".";
        }

        public void Examine()
        {
            Output.WriteLine(Description);
        }
    }
}
