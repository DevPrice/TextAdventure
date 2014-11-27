using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;

namespace TextAdventure.Items
{
    public interface IUsable
    {
        void Use(Entity user);
    }
}
