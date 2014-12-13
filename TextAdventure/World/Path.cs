using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Commands;
using TextAdventure.Utility;

namespace TextAdventure.World
{
    public class Path : IExaminable
    {
        public IMapNode From { get; private set; }
        public IMapNode To { get; private set; }
        public string Identifier { get; set; }
        public string Description { get; set; }

        public Path(string identifier, IMapNode from, IMapNode to)
        {
            From = from;
            To = to;
            Identifier = identifier;
            Description = "A path.";
        }

        public void Examine(ICommandSender examiner)
        {
            examiner.SendMessage("[{0}]: {1}", Identifier, Description);
        }

        public static List<Path> Find(IGameMap map, IMapNode start, IMapNode goal)
        {
            if (start == goal)
                return new List<Path>();

            Dictionary<IMapNode, int> distance = new Dictionary<IMapNode, int>();
            Dictionary<IMapNode, Path> previous = new Dictionary<IMapNode, Path>();

            ICollection<IMapNode> unvisited = new HashSet<IMapNode>();

            foreach (IMapNode node in map.Nodes)
            {
                distance.Add(node, Int32.MaxValue);
                previous.Add(node, null);
                unvisited.Add(node);
            }

            distance[start] = 0;

            while (unvisited.Count > 0)
            {
                IMapNode currentNode = unvisited.First(x => distance[x] == unvisited.Min(y => distance[y]));
                unvisited.Remove(currentNode);

                if (currentNode == goal)
                    break;

                foreach (Path p in map.GetPathsFrom(currentNode))
                {
                    IMapNode neighbor = p.To;
                    int alternateDistance = distance[currentNode] + 1;
                    if (alternateDistance < distance[neighbor])
                    {
                        distance[neighbor] = alternateDistance;
                        previous[neighbor] = p;
                    }
                }
            }

            List<Path> path = new List<Path>();
            Path prevPath = previous[goal];

            do
            {
                path.Insert(0, prevPath);
                prevPath = previous[prevPath.From];
            } while (prevPath != null);

            return path;
        }
    }
}
