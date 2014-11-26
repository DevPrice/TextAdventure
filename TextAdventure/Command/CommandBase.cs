﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.World;

namespace TextAdventure.Command
{
    public abstract class CommandBase : ICommand
    {
        public string Name { get; protected set; }
        public List<string> Aliases { get; protected set; }
        public CommandPermission RequiredPermission { get; protected set; }
        public bool Hidden { get; set; }

        public CommandBase()
        {
            Aliases = new List<string>();
        }

        public abstract void Execute(ICommandSender sender);

        public abstract ICommand Create(string[] args);
    }

    public abstract class CommandBase<T> : CommandBase
    {
        public T Target { get; protected set; }

        public CommandBase(T target)
        {
            Target = target;
        }
    }

    public enum CommandPermission { None, User, Admin }
}
