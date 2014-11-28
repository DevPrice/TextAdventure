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
        IMapNode LocationOf(Entity player);

        List<IMapNode> GetPathsFrom(IMapNode node);
    }
}
