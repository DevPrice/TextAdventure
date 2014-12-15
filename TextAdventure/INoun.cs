using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;

namespace TextAdventure
{
    public interface INoun : INamed
    {
        Gender Gender { get; }
        Article Article { get; }
    }

    public enum Gender { Neutral, Male, Female }
    public enum Article { [RantPatternAttribute(@"")] None, [RantPatternAttribute(@"the")] Definite, [RantPatternAttribute(@"\a")] Indefinite }
}
