using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Commands;
using TextAdventure.Items;
using TextAdventure.Utility;
using TextAdventure.World;

namespace TextAdventure.Entities
{
    public abstract class Entity : IExaminable, IUpdatable
    {
        public string Name { get; set; }
        public EntityAttributes Attributes { get; set; }
        public Gender Gender { get; set; }
        private float _Hp;
        public float Hp
        {
            get { return _Hp; }
            set
            {
                _Hp = value.Clamp(0, Attributes.MaxHp);
            }
        }
        public List<Item> Inventory { get; set; }

        public Entity()
        {
            Attributes = new EntityAttributes();
            Hp = Attributes.MaxHp;
            Inventory = new List<Item>();
        }

        public Entity(int hp)
            : this()
        {
            Hp = Attributes.MaxHp = hp;
        }

        public virtual void Examine(ICommandSender examiner)
        {

        }

        public virtual void Update(TimeSpan delta)
        {

        }
    }

    public enum Gender { Neutral, Male, Female }
}
