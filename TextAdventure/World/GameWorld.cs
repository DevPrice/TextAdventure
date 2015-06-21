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

            int numShields = (int)(Map.Nodes.Count() * .06);

            for (int i = 0; i < numShields; i++)
            {
                Map.GetRandomNode(random).Add(new ItemShield());
            }

            int numFood = (int)(Map.Nodes.Count() * .25);

            for (int i = 0; i < numFood; i++)
            {
                ItemFood food;

                double rand = random.NextDouble();

                if (rand < .2)
                {
                    food = new ItemFood("pizza", 10);
                }
                else if (rand < .25)
                {
                    food = new ItemFood("Taco Bell cheesy gordita crunch", 20);
                }
                else
                {
                    food = new ItemFood("hotdog", 4);
                }

                Map.GetRandomNode(random).Add(food);
            }

            int numPhoenixDown = (int)(Map.Nodes.Count() * .02);

            for (int i = 0; i < numPhoenixDown; i++)
            {
                Map.GetRandomNode(random).Add(new ItemPhoenixDown());
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

            Map.GetRandomNode(random).Add(new EntityBam(this));
        }

        public void Update(TimeSpan delta)
        {
            Map.Update(delta);
        }
    }
}
