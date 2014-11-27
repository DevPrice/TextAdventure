using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Behaviors
{
    public class Behavior : IBehavior, IComparable
    {
        public int Priority { get; set; }
        public bool Active { get; protected set; }
        public int Mask { get; set; }

        public virtual bool ShouldUpdate
        {
            get { return true; }
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

        public int CompareTo(object obj)
        {
            if (obj is Behavior)
                return Priority.CompareTo(((Behavior)obj).Priority);

            throw new ArgumentException("Object is not a Behavior.");
        }
    }
}
