using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;

namespace TextAdventure.World
{
    public interface IGameMap
    {
        IMapNode EntryNode { get; }

        IMapNode LocationOf(Entity player);

        List<Path> GetPathsFrom(IMapNode node);

        List<Path> FindPath(IMapNode from, IMapNode to);
    }
}
