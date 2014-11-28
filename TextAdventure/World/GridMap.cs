using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;

namespace TextAdventure.World
{
    public class GridMap : IGameMap
    {
        public IMapNode LocationOf(Entity entity)
        {
            return new Tile();
        }

        public List<IMapNode> GetPathsFrom(IMapNode node)
        {
            if (node == null || !(node is Tile))
                throw new ArgumentException("Node is not a tile.");

            throw new NotImplementedException();
        }

        public static GridMap Generate(int width, int height)
        {
            return new GridMap();
        }
    }
}
