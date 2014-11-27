using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.World;

namespace TextAdventure.Entities
{
    public class EntityRat : Entity
    {
        public EntityRat()
        {
            Hp = Attributes.MaxHp = 4;
        }
    }
}
