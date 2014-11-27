using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Behaviors
{
    public interface IBehavior
    {
        int Priority { get; }
        bool Active { get; }
        int Mask { get; }
        bool ShouldUpdate { get; }

        void Start();
        void Update();
        void Stop();
    }
}
