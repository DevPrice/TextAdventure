using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;
using TextAdventure.Items;

namespace TextAdventure.World
{
    public class GridMap : IGameMap
    {
        public GridTile[,] Tiles { get; private set; }
        public readonly int Width;
        public readonly int Height;

        public IEnumerable<IMapNode> Nodes
        {
            get
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        yield return Tiles[x, y];
                    }
                }
            }
        }

        public IMapNode EntryNode { get; protected set; }

        public GridMap(int width, int height)
        {
            Tiles = new GridTile[width, height];
            Width = width;
            Height = height;
        }

        public static GridMap Generate(Random random)
        {
            return Generate(random, random.Next(10, 20), random.Next(10, 20));
        }

        public static GridMap Generate(Random random, int width, int height)
        {
            GridMap map = new GridMap(width, height);

            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    map.Tiles[x, y] = new GridTile();
                    map.Tiles[x, y].Travelable = random.Next(4) > 0;
                }
            }

            map.EntryNode = map.Tiles[random.Next(width), random.Next(height)];
            ((GridTile)map.EntryNode).Travelable = true;

            return map;
        }

        public IMapNode LocationOf(Entity entity)
        {
            foreach (GridTile tile in Tiles)
            {
                if (tile.Entities.Contains(entity))
                    return tile;
            }

            return null;
        }

        public IMapNode LocationOf(Item item)
        {
            foreach (GridTile tile in Tiles)
            {
                if (tile.Items.Contains(item))
                    return tile;
            }

            return null;
        }

        private Point GetPosition(IMapNode node)
        {
            Point nodePos = new Point(-1, -1);

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (Tiles[x, y] == node)
                    {
                        nodePos = new Point(x, y);
                        break;
                    }
                }

                if (nodePos.X >= 0)
                    break;
            }

            return nodePos;
        }

        public List<Path> GetPathsFrom(IMapNode node)
        {
            if (node == null || !(node is GridTile))
                throw new ArgumentException("Node is not a tile.");

            Point nodePos = GetPosition(node);

            if (nodePos.X < 0)
                throw new ArgumentException("Node not part of this map.");

            List<Path> pathsFrom = new List<Path>();

            if (nodePos.Y > 0)
                pathsFrom.Add(new Path("north", node, Tiles[nodePos.X, nodePos.Y - 1]));

            if (nodePos.Y < Height - 1)
                pathsFrom.Add(new Path("south", node, Tiles[nodePos.X, nodePos.Y + 1]));

            if (nodePos.X > 0)
                pathsFrom.Add(new Path("west", node, Tiles[nodePos.X - 1, nodePos.Y]));

            if (nodePos.X < Width - 1)
                pathsFrom.Add(new Path("east", node, Tiles[nodePos.X + 1, nodePos.Y]));

            pathsFrom.RemoveAll(x => !((GridTile)x.To).Travelable);

            return pathsFrom;
        }


        public List<Path> FindPath(IMapNode from, IMapNode to)
        {
            return Path.Find(this, from, to);
        }

        public void MoveEntity(Entity entity, Path path)
        {
            if (path.From != null)
                path.From.Remove(entity, path);

            path.To.Add(entity, path);
        }

        public void Update(TimeSpan delta)
        {
            foreach (IMapNode node in Tiles)
                node.Update(delta);
        }

        public IMapNode GetRandomNode(Random random)
        {
            return Tiles[random.Next(Width), random.Next(Height)];
        }
    }

    struct Point
    {
        public readonly int X;
        public readonly int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
