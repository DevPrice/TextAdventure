using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.World
{
    public interface IGameMap
    {
        List<IMapNode> GetNeighbors(IMapNode node);
    }
}
