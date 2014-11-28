using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Behaviors;

namespace TextAdventure.Entities
{
    public class EntityNPC : Entity
    {
        public List<Behavior> Behaviors { get; set; }
    }
}
