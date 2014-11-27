using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Utility;
using TextAdventure.World;

namespace TextAdventure.Entity
{
    public abstract class Entity
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

        public Entity()
        {
            Attributes = new EntityAttributes();
            Hp = Attributes.MaxHp;
        }
    }

    public enum Gender { Neutral, Male, Female }
}
