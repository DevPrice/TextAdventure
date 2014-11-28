using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;
using TextAdventure.Items;

namespace TextAdventure.World
{
    public interface IMapNode : IExaminable
    {
        List<Entity> Entities { get; }
        List<Item> Items { get; }
    }
}
