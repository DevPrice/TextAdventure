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
        public Random Random { get; private set; }

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

            world.Random = new Random(random.Next());

            world.GenerateItems(random);
            world.GenerateEntities(random);

            return world;
        }

        private void GenerateItems(Random random)
        {
            int numSwords = (int)(Map.Nodes.Count() * .08);

            for (int i = 0; i < numSwords; i++)
            {
                Map.GetRandomNode(random).Add(new ItemSword());
            }

            int numFood = (int)(Map.Nodes.Count() * .25);

            for (int i = 0; i < numFood; i++)
            {
                ItemFood food = random.NextDouble() < .2 ? new ItemFood("pizza", 10) : new ItemFood("hotdog", 4);

                Map.GetRandomNode(random).Add(food);
            }
        }

        private void GenerateEntities(Random random)
        {
            int numRats = (int)(Map.Nodes.Count() * .4);

            for (int i = 0; i < numRats; i++)
            {
                Map.GetRandomNode(random).Add(new EntityRat(this));
            }

            int numCats = (int)(Map.Nodes.Count() * .2);

            for (int i = 0; i < numCats; i++)
            {
                Map.GetRandomNode(random).Add(new EntityCat(this));
            }

            int numBats = (int)(Map.Nodes.Count() * .2);

            for (int i = 0; i < numBats; i++)
            {
                Map.GetRandomNode(random).Add(new EntityBat(this));
            }
        }

        public void Update(TimeSpan delta)
        {
            Map.Update(delta);
        }
    }
}
