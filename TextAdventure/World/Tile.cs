using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Commands;
using TextAdventure.Entities;
using TextAdventure.Events;
using TextAdventure.Items;
using TextAdventure.Utility;

namespace TextAdventure.World
{
    public class Tile : IMapNode
    {
        private List<Entity> _Entities;
        public IReadOnlyCollection<Entity> Entities { get { return _Entities.AsReadOnly(); } }

        private List<Item> _Items;
        public IReadOnlyCollection<Item> Items { get { return _Items.AsReadOnly(); } }

        #region events

        public event EventHandler<EntityMovedEventArgs> EntityEntered;
        public event EventHandler<EntityMovedEventArgs> EntityLeft;

        #endregion

        public Tile()
        {
            _Entities = new List<Entity>();
            _Items = new List<Item>();

            EntityEntered += OnEntityEntered;
            EntityLeft += OnEntityLeft;
        }

        public void Examine(ICommandSender examiner)
        {
            examiner.SendMessage("You are on a tile.");
        }

        public void Update(TimeSpan delta)
        {
            foreach (Entity entity in Entities)
                entity.Update(delta);

            foreach (Item item in Items)
                item.Update(delta);
        }

        public void Add(Entity entity)
        {
            _Entities.Add(entity);

            if (EntityEntered != null)
                EntityEntered(this, new EntityMovedEventArgs(entity, null, this));
        }

        public void Remove(Entity entity)
        {
            bool success = _Entities.Remove(entity);

            if (success && EntityLeft != null)
                EntityLeft(this, new EntityMovedEventArgs(entity, this, null));
        }

        public void Add(Item item)
        {
            _Items.Add(item);
        }

        public void Remove(Item item)
        {
            _Items.Remove(item);
        }

        private void OnEntityEntered(object sender, EntityMovedEventArgs e)
        {
            string message = String.Format("{0} entered the area.", e.Entity.Name);
            
            if (e.Entity is Player)
            {
                Broadcast(message, (Player)e.Entity);
            }
            else
            {
                Broadcast(message);
            }
        }

        private void OnEntityLeft(object sender, EntityMovedEventArgs e)
        {
            string message = String.Format("{0} left the area.", e.Entity.Name);

            if (e.Entity is Player)
            {
                Broadcast(message, (Player)e.Entity);
            }
            else
            {
                Broadcast(message);
            }
        }


        public void Broadcast(string message)
        {
            Broadcast(message, null);
        }

        public void Broadcast(string message, Player player)
        {
            foreach (Entity entity in Entities)
            {
                if (entity is Player && entity != player)
                    ((Player)entity).SendMessage(message);
            }
        }
    }
}
