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

            world.GenerateItems(random);
            world.GenerateEntities(random);

            return world;
        }

        private void GenerateItems(Random random)
        {
            int numSwords = (int)(Map.NumNodes * .8);

            for (int i = 0; i < numSwords; i++)
            {
                Map.GetRandomNode(random).Add(new ItemSword());
            }

            int numFood = (int)(Map.NumNodes * .8);

            for (int i = 0; i < numFood; i++)
            {
                ItemFood food = random.NextDouble() < .2 ? new ItemFood("pizza", 10) : new ItemFood("hotdog", 4);

                Map.GetRandomNode(random).Add(food);
            }
        }

        private void GenerateEntities(Random random)
        {
            int numRats = (int)(Map.NumNodes * .4);

            for (int i = 0; i < numRats; i++)
            {
                Map.GetRandomNode(random).Add(new EntityRat(this));
            }
        }

        public void Update(TimeSpan delta)
        {
            Map.Update(delta);
        }
    }
}
