using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;
using TextAdventure.Items;

namespace TextAdventure.World
{
    public class GameWorld : IUpdatable
    {
        public IGameMap Map { get; private set; }
        public List<Player> Players { get; private set; }

        protected GameWorld(IGameMap map)
        {
            Map = map;
            Players = new List<Player>();
        }

        public static GameWorld Generate()
        {
            return Generate(new Random());
        }

        public static GameWorld Generate(int seed)
        {
            return Generate(new Random(seed));
        }

        public static GameWorld Generate(Random random)
        {
            IGameMap map = GridMap.Generate(random);
            map.EntryNode.Add(new Item("compass"));

            GameWorld world = new GameWorld(map);
            map.EntryNode.Add(new EntityRat(world));

            return world;
        }

        public void Update(TimeSpan delta)
        {
            Map.Update(delta);
        }
    }
}
