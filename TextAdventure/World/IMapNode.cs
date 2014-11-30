using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;
using TextAdventure.Events;
using TextAdventure.Items;

namespace TextAdventure.World
{
    public interface IMapNode : IExaminable, IUpdatable
    {
        IReadOnlyCollection<Entity> Entities { get; }
        IReadOnlyCollection<Item> Items { get; }

        event EventHandler<EntityMovedEventArgs> EntityEntered;
        event EventHandler<EntityMovedEventArgs> EntityLeft;

        void Add(Entity entity);
        void Remove(Entity entity);
        void Add(Item item);
        void Remove(Item item);

        void Broadcast(string message);
        void Broadcast(string message, Player player);
    }
}
