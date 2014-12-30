using System;
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

        public int Level { get; private set; }

        public event EventHandler<LevelUpEventArgs> LevelUp;

        public Player(GameWorld world)
            : base(world, 10)
        {
            Article = Article.None;
            Name = "You";
            Gender = Gender.Male;
            Permission = CommandPermission.User;
            Level = 1;

            DamageTaken += OnDamageTaken;
            Death += OnDeath;
            AttackedEntity += OnAttackedEntity;
            AttackMissed += OnAttackMissed;
            KilledEntity += OnKilledEntity;
            Equipment.ItemEquipped += OnItemEquipped;
            Equipment.ItemUnequipped += OnItemUnequipped;
            LevelUp += OnLevelUp;
        }

        public void GiveExperience(Entity killedEntity)
        {
            Experience += killedEntity.Experience;

            while (global::TextAdventure.Entities.Experience.GetLevelFromExp(Experience) > Level)
            {
                Level++;

                if (LevelUp != null)
                    LevelUp(this, new LevelUpEventArgs(killedEntity, killedEntity.Experience));
            }
        }

        private void OnDamageTaken(object sender, DamageTakenEventArgs e)
        {
            int displayDamage = (int)Math.Min(Attributes.MaxHp, Hp + e.Amount) - (int)Hp;
            SendLine("{0} damage taken from {1}.", displayDamage, e.DamageSource);
        }

        private void OnDeath(object sender, DamageTakenEventArgs e)
        {
            SendLine("YOU DIED");
        }

        private void OnAttackedEntity(object sender, AttackedEntityEventArgs e)
        {
            int displayDamage = (int)(e.AttackedEntity.Hp + e.DamageDealt) - (int)e.AttackedEntity.Hp;
            SendLine("You attack {0} for {1} damage.", e.AttackedEntity.Name, displayDamage);
        }

        private void OnAttackMissed(object sender, AttackMissedEventArgs e)
        {
            SendLine("You miss!");
        }

        private void OnKilledEntity(object sender, AttackedEntityEventArgs e)
        {
            SendLine("You killed {0}.", e.AttackedEntity.Name);

            GiveExperience(e.AttackedEntity);
        }

        private void OnLevelUp(object sender, LevelUpEventArgs e)
        {
            SendLine("You leveled up!");

            BaseAttributes += PerLevel.Human;
            Hp += PerLevel.Human.MaxHp;
        }

        private void OnItemEquipped(object sender, ItemEquipEventArgs e)
        {
            SendLine("{0} equipped.", e.Item.Name.ToTitleCase());
        }

        private void OnItemUnequipped(object sender, ItemEquipEventArgs e)
        {
            SendLine("{0} unequipped.", e.Item.Name.ToTitleCase());
        }

        public void Send()
        {
            Send(String.Empty);
        }

        public virtual void Send(string message)
        {
            if (Alive)
                Output.WriteLine(message);
        }

        public void Send(string message, params object[] args)
        {
            Send(String.Format(message, args));
        }

        public void SendLine()
        {
            SendLine(String.Empty);
        }

        public void SendLine(string message)
        {
            Send(message + Environment.NewLine);
        }

        public void SendLine(string value, params object[] args)
        {
            SendLine(String.Format(value, args));
        }
    }
}
