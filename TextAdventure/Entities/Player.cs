using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Behaviors;
using TextAdventure.Commands;
using TextAdventure.Events;
using TextAdventure.Utility;
using TextAdventure.World;

namespace TextAdventure.Entities
{
    public class Player : Entity, ICommandSender
    {
        public CommandPermission Permission { get; set; }

        public Player(GameWorld world)
            : base(world, 10)
        {
            Name = "You";
            Permission = CommandPermission.User;
            DamageTaken += OnDamageTaken;
            Death += OnDeath;
            AttackedEntity += OnAttackedEntity;
            KilledEntity += OnKilledEntity;
        }

        private void OnDamageTaken(object sender, DamageTakenEventArgs e)
        {
            int displayDamage = (int)Math.Max(Attributes.MaxHp, Hp + e.Amount) - (int)Hp;
            SendMessage("{0} damage taken from {1}.", displayDamage, e.DamageSource);
        }

        private void OnDeath(object sender, DamageTakenEventArgs e)
        {
            SendMessage("YOU DIED");
        }

        private void OnAttackedEntity(object sender, AttackedEntityEventArgs e)
        {
            int displayDamage = (int)(e.AttackedEntity.Hp + e.DamageDealt) - (int)e.AttackedEntity.Hp;
            SendMessage("You attack {0} for {1} damage.", e.AttackedEntity.Name, displayDamage);
        }

        private void OnKilledEntity(object sender, AttackedEntityEventArgs e)
        {
            SendMessage("You killed {0}.", e.AttackedEntity.Name);
        }

        public virtual void SendMessage(string message)
        {
            if (Alive)
                Output.WriteLine(message);
        }

        public void SendMessage()
        {
            SendMessage(String.Empty);
        }

        public void SendMessage(string value, params object[] args)
        {
            SendMessage(String.Format(value, args));
        }
    }
}
