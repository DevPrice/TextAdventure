using System;

namespace TextAdventure.World
{
    public interface IUpdatable
    {
        void Update(TimeSpan delta);
    }
}
