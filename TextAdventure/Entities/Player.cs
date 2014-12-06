﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Behaviors;
using TextAdventure.Commands;
using TextAdventure.Events;
using TextAdventure.Items;
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
            AttackMissed += OnAttackMissed;
            KilledEntity += OnKilledEntity;
            Equipment.ItemEquipped += OnItemEquipped;
            Equipment.ItemUnequipped += OnItemUnequipped;
            Inventory.Add(new ItemSword());
        }

        private void OnDamageTaken(object sender, DamageTakenEventArgs e)
        {
            int displayDamage = (int)Math.Min(Attributes.MaxHp, Hp + e.Amount) - (int)Hp;
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

        private void OnAttackMissed(object sender, AttackMissedEventArgs e)
        {
            SendMessage("You miss!");
        }

        private void OnKilledEntity(object sender, AttackedEntityEventArgs e)
        {
            SendMessage("You killed {0}.", e.AttackedEntity.Name);
        }

        private void OnItemEquipped(object sender, ItemEquipEventArgs e)
        {
            SendMessage("{0} equipped.", e.Item.Name.ToTitleCase());
        }

        private void OnItemUnequipped(object sender, ItemEquipEventArgs e)
        {
            SendMessage("{0} unequipped.", e.Item.Name.ToTitleCase());
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
