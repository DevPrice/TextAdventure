using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Utility;

namespace TextAdventure.World
{
    public class Path : IExaminable
    {
        public IMapNode From { get; private set; }
        public IMapNode To { get; private set; }
        public string Identifier { get; set; }
        public string Description { get; set; }

        public Path(IMapNode from, IMapNode to)
        {
            From = from;
            To = to;
        }

        public void Examine()
        {
            Output.WriteLine(Description ?? "A path.");
        }
    }
}
