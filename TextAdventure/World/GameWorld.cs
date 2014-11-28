using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;

namespace TextAdventure.World
{
    public class GameWorld
    {
        public IGameMap Map { get; private set; }
        public List<Player> Players { get; private set; }

        private GameWorld(IGameMap map)
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
            return new GameWorld(map);
        }
    }
}
