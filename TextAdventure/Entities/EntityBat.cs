﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Behaviors;
using TextAdventure.World;

namespace TextAdventure.Entities
{
    public class EntityBat : EntityNPC
    {
        public EntityBat(GameWorld world)
            : base(world, 4)
        {
            Name = "bat";
            BaseAttributes.Strength = .6;
            BaseAttributes.Defense = 0;
            BaseAttributes.Accuracy = 5;
            BaseAttributes.Dodge = 20;
            BaseAttributes.Speed = 1;

            Behaviors.Add(new BehaviorHostileToPlayers(this));
        }
    }
}
