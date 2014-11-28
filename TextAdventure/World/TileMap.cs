using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.World
{
    public class TileMap : IGameMap
    {
        public List<IMapNode> GetNeighbors(IMapNode node)
        {
            if (node == null || !(node is Tile))
                throw new ArgumentException("Node is not a tile.");

            throw new NotImplementedException();
        }
    }
}
