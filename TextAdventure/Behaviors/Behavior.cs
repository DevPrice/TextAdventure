using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Behaviors
{
    public class Behavior : IBehavior
    {
        public int Priority { get; set; }
        public bool Active { get; protected set; }
        public int Mask { get; set; }

        public bool ShouldUpdate
        {
            get { throw new NotImplementedException(); }
        }

        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Stop()
        {

        }
    }
}
