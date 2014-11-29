using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Commands;
using TextAdventure.Entities;
using TextAdventure.Items;
using TextAdventure.Utility;

namespace TextAdventure.World
{
    public class Tile : IMapNode
    {

        public List<Entity> Entities { get; private set; }

        public List<Item> Items { get; private set; }

        public Tile()
        {
            Entities = new List<Entity>();
            Items = new List<Item>();
        }

        public void Examine(ICommandSender examiner)
        {
            examiner.SendMessage("You are on a tile.");
        }

        public void Update(TimeSpan delta)
        {
            foreach (Entity entity in Entities)
                entity.Update(delta);

            foreach (Item item in Items)
                item.Update(delta);
        }
    }
}
