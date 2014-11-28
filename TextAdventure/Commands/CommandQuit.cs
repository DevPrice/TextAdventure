﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.World;

namespace TextAdventure.Commands
{
    public class CommandQuit : Command
    {
        public CommandQuit(GameWorld world)
            : base(world)
        {
            Name = "quit";
            Aliases.Add("exit");
        }

        public override void Execute(ICommandSender sender)
        {
            base.Execute(sender);

            Environment.Exit(Environment.ExitCode);
        }

        public override ICommand Create(string[] args)
        {
            return new CommandQuit(World);
        }
    }
}
