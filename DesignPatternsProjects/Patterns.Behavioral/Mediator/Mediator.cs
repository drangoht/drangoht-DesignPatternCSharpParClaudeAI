using System;
using System.Collections.Generic;

namespace Patterns.Behavioral.Mediator
{
    /// <summary>
    /// Mediator - Interface définissant les méthodes de communication entre les collègues
    /// </summary>
    public interface IChatMediator
    {
        void AddUser(ChatUser user);
        void SendMessage(string message, ChatUser sender);
        void SendDirectMessage(string message, ChatUser sender, ChatUser recipient);
    }

    /// <summary>
    /// ConcreteMediator - Implémente le comportement de coordination entre les collègues
    /// </summary>
    public class ChatRoom : IChatMediator
    {
        private readonly List<ChatUser> _users = new List<ChatUser>();
        private readonly string _roomName;

        public ChatRoom(string roomName)
        {
            _roomName = roomName;
            Console.WriteLine($"Salon de discussion '{_roomName}' créé");
        }

        public void AddUser(ChatUser user)
        {
            if (!_users.Contains(user))
            {
                _users.Add(user);
                Console.WriteLine($"[Système] {user.Name} a rejoint le salon '{_roomName}'");
                
                // Notification aux autres utilisateurs
                foreach (var existingUser in _users)
                {
                    if (existingUser != user)
                    {
                        existingUser.ReceiveNotification($"{user.Name} a rejoint le salon");
                    }
                }
            }
        }

        public void SendMessage(string message, ChatUser sender)
        {
            Console.WriteLine($"\n[{_roomName}] {sender.Name}: {message}");
            
            // Diffusion du message à tous les utilisateurs sauf l'expéditeur
            foreach (var user in _users)
            {
                if (user != sender)
                {
                    user.ReceiveMessage(message, sender);
                }
            }
        }

        public void SendDirectMessage(string message, ChatUser sender, ChatUser recipient)
        {
            if (_users.Contains(recipient))
            {
                Console.WriteLine($"\n[{_roomName}] [Message privé] {sender.Name} → {recipient.Name}: {message}");
                recipient.ReceiveDirectMessage(message, sender);
            }
            else
            {
                sender.ReceiveNotification($"Impossible d'envoyer un message à {recipient.Name} - utilisateur non trouvé");
            }
        }
    }

    /// <summary>
    /// Colleague - Classe abstraite définissant l'interface commune aux collègues
    /// </summary>
    public abstract class ChatUser
    {
        protected IChatMediator _mediator;
        public string Name { get; }
        public UserType Type { get; }

        protected ChatUser(string name, UserType type, IChatMediator mediator)
        {
            Name = name;
            Type = type;
            _mediator = mediator;
        }

        public virtual void Send(string message)
        {
            _mediator.SendMessage(message, this);
        }

        public virtual void SendDirect(string message, ChatUser recipient)
        {
            _mediator.SendDirectMessage(message, this, recipient);
        }

        public abstract void ReceiveMessage(string message, ChatUser sender);
        public abstract void ReceiveDirectMessage(string message, ChatUser sender);
        public abstract void ReceiveNotification(string notification);
    }

    /// <summary>
    /// Énumération des types d'utilisateurs
    /// </summary>
    public enum UserType
    {
        Regular,
        Premium,
        Moderator,
        Admin
    }

    /// <summary>
    /// ConcreteColleague - Implémentation concrète d'un collègue
    /// </summary>
    public class RegularUser : ChatUser
    {
        public RegularUser(string name, IChatMediator mediator) 
            : base(name, UserType.Regular, mediator)
        {
            mediator.AddUser(this);
        }

        public override void ReceiveMessage(string message, ChatUser sender)
        {
            Console.WriteLine($"[{Name} a reçu]: {sender.Name}: {message}");
        }

        public override void ReceiveDirectMessage(string message, ChatUser sender)
        {
            Console.WriteLine($"[{Name} a reçu un message privé de {sender.Name}]: {message}");
        }

        public override void ReceiveNotification(string notification)
        {
            Console.WriteLine($"[Notification pour {Name}]: {notification}");
        }
    }

    /// <summary>
    /// ConcreteColleague - Autre implémentation avec des comportements spécifiques
    /// </summary>
    public class AdminUser : ChatUser
    {
        public AdminUser(string name, IChatMediator mediator) 
            : base(name, UserType.Admin, mediator)
        {
            mediator.AddUser(this);
        }

        public void BroadcastAnnouncement(string announcement)
        {
            string formattedAnnouncement = $"[ANNONCE] {announcement}";
            _mediator.SendMessage(formattedAnnouncement, this);
        }

        public override void ReceiveMessage(string message, ChatUser sender)
        {
            Console.WriteLine($"[Admin {Name} a reçu]: {sender.Name} ({sender.Type}): {message}");
        }

        public override void ReceiveDirectMessage(string message, ChatUser sender)
        {
            Console.WriteLine($"[Admin {Name} a reçu un message privé de {sender.Name}]: {message}");
        }

        public override void ReceiveNotification(string notification)
        {
            Console.WriteLine($"[Notification pour Admin {Name}]: {notification}");
        }
    }

    /// <summary>
    /// ConcreteColleague - Troisième implémentation avec des comportements spécifiques
    /// </summary>
    public class ModeratorUser : ChatUser
    {
        public ModeratorUser(string name, IChatMediator mediator) 
            : base(name, UserType.Moderator, mediator)
        {
            mediator.AddUser(this);
        }

        public void ModerateMessage(string moderationMessage)
        {
            string formattedMessage = $"[MODÉRATION] {moderationMessage}";
            _mediator.SendMessage(formattedMessage, this);
        }

        public override void ReceiveMessage(string message, ChatUser sender)
        {
            Console.WriteLine($"[Modérateur {Name} a reçu]: {sender.Name} ({sender.Type}): {message}");
        }

        public override void ReceiveDirectMessage(string message, ChatUser sender)
        {
            Console.WriteLine($"[Modérateur {Name} a reçu un message privé de {sender.Name}]: {message}");
        }

        public override void ReceiveNotification(string notification)
        {
            Console.WriteLine($"[Notification pour Modérateur {Name}]: {notification}");
        }
    }
}


