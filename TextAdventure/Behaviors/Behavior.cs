using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;
using TextAdventure.World;

namespace TextAdventure.Behaviors
{
    public abstract class Behavior : IBehavior, IComparable, IUpdatable
    {
        public int Priority { get; set; }
        public bool Active { get; protected set; }
        public int Mask { get; set; }
        public Entity Entity { get; private set; }
        public virtual bool ShouldUpdate { get; protected set; }

        public Behavior(Entity entity)
        {
            Entity = entity;
        }

        public virtual void Start()
        {
            Active = true;
        }

        public virtual void Update(TimeSpan delta)
        {

        }

        public virtual void Stop()
        {
            Active = false;
        }

        public int CompareTo(object obj)
        {
            if (obj is Behavior)
                return Priority.CompareTo(((Behavior)obj).Priority);

            throw new ArgumentException("Object is not a Behavior.");
        }
    }

    public enum BehaviorMask { None = 0, Combat = 1, Movement = 2 }
}
