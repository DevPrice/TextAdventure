using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.World;

namespace TextAdventure.Commands
{
    public abstract class Command : ICommand, ICommandFactory
    {
        public string Name { get; protected set; }
        public List<string> Aliases { get; protected set; }
        public string Description { get; protected set; }
        public string Usage { get; protected set; }
        public CommandPermission RequiredPermission { get; protected set; }
        public bool Hidden { get; set; }
        public GameWorld World { get; private set; }

        public Command(GameWorld world)
        {
            Aliases = new List<string>();
            RequiredPermission = CommandPermission.User;
            World = world;
        }

        public virtual void Execute(ICommandSender sender)
        {
            if ((int)sender.Permission < (int)RequiredPermission)
                throw new InsufficientPermissionException(sender);
        }

        public abstract ICommand Create(string[] args);
    }

    public abstract class Command<T> : Command
    {
        public T Target { get; private set; }

        public Command(GameWorld world, T target)
            : base(world)
        {
            Target = target;
        }
    }

    public enum CommandPermission { None, User, Admin }
}
