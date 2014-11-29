using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;
using TextAdventure.Items;

namespace TextAdventure.World
{
    public interface IGameMap : IUpdatable
    {
        IMapNode EntryNode { get; }

        IMapNode LocationOf(Entity entity);

        IMapNode LocationOf(Item item);

        List<Path> GetPathsFrom(IMapNode node);

        List<Path> FindPath(IMapNode from, IMapNode to);
    }
}
