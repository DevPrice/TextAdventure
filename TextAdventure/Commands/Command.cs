using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.World;

namespace TextAdventure.Commands
{
    public abstract class Command : ICommand, ICommandFactory, INamed
    {
        public string Name { get; protected set; }
        public List<string> Aliases { get; protected set; }
        public string Description { get; protected set; }
        public string Usage { get; protected set; }
        public ICommandSender Sender { get; private set; }
        public CommandPermission RequiredPermission { get; protected set; }
        public bool Hidden { get; set; }
        protected GameWorld World { get; private set; }

        public Command(GameWorld world, ICommandSender sender)
        {
            Aliases = new List<string>();
            RequiredPermission = CommandPermission.User;
            World = world;
            Sender = sender;
        }

        public virtual void Execute()
        {
            if ((int)Sender.Permission < (int)RequiredPermission)
                throw new InsufficientPermissionException(Sender);
        }

        public abstract ICommand Create(ICommandSender sender, string[] args);
    }

    public abstract class Command<T> : Command
    {
        public T Target { get; private set; }

        public Command(GameWorld world, ICommandSender sender, T target)
            : base(world, sender)
        {
            Target = target;
        }
    }

    public enum CommandPermission { None, User, Admin }
}
