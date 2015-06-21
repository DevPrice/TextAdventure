using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.World;

namespace TextAdventure.Sense
{
    public interface IObserver
    {
        bool CanObserve(IObservable observerable);

        void Observe(IObservable observerable);
    }
}
