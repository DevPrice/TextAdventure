using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;
using TextAdventure.World;

namespace TextAdventure.Commands
{
    public class CommandSay : Command<string>
    {
        public CommandSay(GameWorld world, ICommandSender sender, string message)
            : base(world, sender, message)
        {
            Name = "say";
            Aliases.Add("shout");
            Aliases.Add("speak");
        }

        public override void Execute()
        {
            base.Execute();

            if (Sender is Player)
            {
                string message = String.Format("{0} says, \"{1}\"", ((Player)Sender).Name, Target);
                World.Map.LocationOf((Player)Sender).Broadcast(message, (Player)Sender);

                Sender.SendLine("You say, \"{0}\"", Target);
            }
            else
            {
                foreach (Player player in World.Players)
                {
                    player.SendLine("SERVER: {0}", Target);
                }
            }
        }

        public override ICommand Create(ICommandSender sender, string[] args)
        {
            string[] messageArr = new string[args.Length - 1];
            Array.Copy(args, 1, messageArr, 0, messageArr.Length);
            string message = String.Join(" ", messageArr);

            return new CommandSay(World, sender, message);
        }
    }
}
