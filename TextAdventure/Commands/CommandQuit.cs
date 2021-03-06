﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;
using TextAdventure.World;

namespace TextAdventure.Commands
{
    public class CommandQuit : Command
    {
        public CommandQuit(GameWorld world, ICommandSender sender)
            : base(world, sender)
        {
            Name = "quit";
            Aliases.Add("exit");
            RequiredPermission = CommandPermission.Admin;
        }

        public override void Execute()
        {
            base.Execute();

            if (Sender is GameServer)
            {
                ((GameServer)Sender).Stop();
            }
        }

        public override ICommand Create(ICommandSender sender, string[] args)
        {
            return new CommandQuit(World, sender);
        }
    }
}
