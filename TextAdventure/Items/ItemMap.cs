using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;
using TextAdventure.World;

namespace TextAdventure.Items
{
    public class ItemMap : Item, IUsable
    {
        private GridMap GridMap;
        private char[,] Map;

        public ItemMap(GridMap gameMap)
            : base("map")
        {
            GridMap = gameMap;
            Map = new char[gameMap.Width, gameMap.Height];

            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    Map[i, j] = ' ';
                }
            }
        }

        public override void OnPickup(Entity entity)
        {
            base.OnPickup(entity);

            Mark(entity.Location);

            entity.Moved += OnEntityMoved;
        }

        private void OnEntityMoved(object sender, Events.EntityMovedEventArgs e)
        {
            Mark(e.Path.To);
        }

        public void Use(Entity user)
        {
            if (user is Player)
            {
                Player p = (Player)user;

                p.SendLine(PrintMap(GridMap.GetPosition(p.Location)));
            }
        }

        private string PrintMap(Point currentLocation)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < GridMap.Width + 2; i++)
            {
                sb.Append('-');
            }
            sb.Append(Environment.NewLine);

            for (int y = 0; y < Map.GetLength(1); y++)
            {
                sb.Append('|');

                for (int x = 0; x < Map.GetLength(0); x++)
                {
                    if (x == currentLocation.X && y == currentLocation.Y)
                    {
                        sb.Append(Char.ToUpper(Map[x, y]));
                    }
                    else
                    {
                        sb.Append(Map[x, y]);
                    }
                }

                sb.Append('|');
                sb.Append(Environment.NewLine);
            }

            for (int i = 0; i < GridMap.Width + 2; i++)
            {
                sb.Append('-');
            }

            return sb.ToString();
        }

        public void Mark(IMapNode tile)
        {
            Point position = GridMap.GetPosition(tile);
            IEnumerable<Path> paths = GridMap.GetPathsFrom(tile);

            Map[position.X, position.Y] = 'x';

            if (paths.FirstOrDefault(x => x.Identifier.Equals("north")) != null
                && Map[position.X, position.Y - 1] == ' ')
            {
                Map[position.X, position.Y - 1] = '?';
            }
            if (paths.FirstOrDefault(x => x.Identifier.Equals("east")) != null
                && Map[position.X + 1, position.Y] == ' ')
            {
                Map[position.X + 1, position.Y] = '?';
            }
            if (paths.FirstOrDefault(x => x.Identifier.Equals("west")) != null
                && Map[position.X - 1, position.Y] == ' ')
            {
                Map[position.X - 1, position.Y] = '?';
            }
            if (paths.FirstOrDefault(x => x.Identifier.Equals("south")) != null
                && Map[position.X, position.Y + 1] == ' ')
            {
                Map[position.X, position.Y + 1] = '?';
            }
        }
    }
}
