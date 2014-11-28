﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;
using TextAdventure.Utility;
using TextAdventure.World;

namespace TextAdventure.Commands
{
    public class CommandStatus : Command<Entity>
    {
        public CommandStatus(GameWorld world, ICommandSender sender, Entity entity)
            : base(world, sender, entity)
        {
            Name = "status";
            Aliases.Add("hp");
        }

        public override void Execute()
        {
            base.Execute();

            Output.WriteLine("HP: {0}", Target.Hp);
        }

        public override ICommand Create(ICommandSender sender, string[] args)
        {
            if (sender is Player)
                return new CommandStatus(World, sender, (Entity)sender);

            throw new UsageException();
        }
    }
}
