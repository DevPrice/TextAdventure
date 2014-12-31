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
    public class GridTile : IMapNode
    {
        public string Adjective { get; private set; } // Just for fun. This is temporary.
        public bool Travelable { get; set; }

        private List<Entity> _Entities;
        public IReadOnlyCollection<Entity> Entities { get { return _Entities.AsReadOnly(); } }

        private List<Item> _Items;
        public IReadOnlyCollection<Item> Items { get { return _Items.AsReadOnly(); } }

        #region events

        public event EventHandler<EntityMovedEventArgs> EntityEntered;
        public event EventHandler<EntityMovedEventArgs> EntityLeft;

        #endregion

        public GridTile()
        {
            _Entities = new List<Entity>();
            _Items = new List<Item>();

            Adjective = RantEngine.RunPattern("<adj>");

            EntityEntered += OnEntityEntered;
            EntityLeft += OnEntityLeft;
        }

        public void Examine(ICommandSender examiner)
        {
            examiner.SendLine(RantEngine.RunPattern("You are on \\a {0} tile.", Adjective));
        }

        public void Update(TimeSpan delta)
        {
            foreach (Entity entity in new List<Entity>(Entities))
                entity.Update(delta);

            foreach (Item item in Items)
                item.Update(delta);
        }

        public void Add(Entity entity)
        {
            Add(entity, null);
        }

        public void Add(Entity entity, Path path)
        {
            _Entities.Add(entity);

            if (EntityEntered != null)
                EntityEntered(this, new EntityMovedEventArgs(entity, path));
        }

        public void Remove(Entity entity)
        {
            Remove(entity, null);
        }

        public void Remove(Entity entity, Path path)
        {
            bool success = _Entities.Remove(entity);

            if (success && EntityLeft != null)
                EntityLeft(this, new EntityMovedEventArgs(entity, path));
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
            // God this is so hacky but I don't want to fix things right now.
            // TODO: Fix this.
            // Seriously, please fix this.
            // Please.
            Dictionary<string, string> reverseDirection = new Dictionary<string,string>();
            reverseDirection.Add("north", "south");
            reverseDirection.Add("south", "north");
            reverseDirection.Add("east", "west");
            reverseDirection.Add("west", "east");

            string message = String.Format("{0} entered the area", e.Entity.GetFullName().FirstCharToUpper());

            if (e.Path != null && e.Path.From != null)
                message += String.Format(" from the [{0}]", reverseDirection[e.Path.Identifier]);

            message += ".";
            
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
            string message = String.Format("{0} left the area.", e.Entity.GetFullName().FirstCharToUpper());

            if (e.Path != null && e.Path.To != null)
                message = String.Format("{0} moved [{1}].", e.Entity.Name.FirstCharToUpper(), e.Path.Identifier);

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
                    ((Player)entity).SendLine(message);
            }
        }
    }
}
