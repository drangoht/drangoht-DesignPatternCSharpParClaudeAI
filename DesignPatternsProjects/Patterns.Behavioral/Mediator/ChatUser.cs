using System;

namespace Patterns.Behavioral.Mediator
{
    /// <summary>
    /// Base abstraite pour les utilisateurs du chat (collègues)
    /// </summary>
    public abstract class ChatUser
    {
        protected readonly IChatMediator _mediator;
        public string Name { get; }

        protected ChatUser(string name, IChatMediator mediator)
        {
            Name = name;
            _mediator = mediator;
            _mediator.Register(this);
        }

        public virtual void Send(string message)
        {
            _mediator.Send(message, this);
        }

        public virtual void SendTo(string message, ChatUser receiver)
        {
            _mediator.SendTo(message, this, receiver);
        }

        public virtual void SendPrivate(string message, ChatUser receiver)
        {
            _mediator.SendPrivate(message, this, receiver);
        }

        public virtual void Broadcast(string announcement)
        {
            _mediator.Broadcast(announcement, this);
        }

        public virtual void Receive(string message, ChatUser sender)
        {
            Console.WriteLine($"{Name} a reçu de {sender.Name}: {message}");
        }

        public virtual void ReceivePrivate(string message, ChatUser sender)
        {
            Console.WriteLine($"{Name} a reçu un message privé de {sender.Name}: {message}");
        }

        public virtual void ReceiveAnnouncement(string announcement, ChatUser sender)
        {
            Console.WriteLine($"{Name} a reçu l'annonce de {sender.Name}: {announcement}");
        }
    }

    /// <summary>
    /// Utilisateur régulier
    /// </summary>
    public class RegularUser : ChatUser
    {
        public RegularUser(string name, IChatMediator mediator) : base(name, mediator)
        {
        }
    }

    /// <summary>
    /// Utilisateur modérateur
    /// </summary>
    public class ModeratorUser : ChatUser
    {
        public ModeratorUser(string name, IChatMediator mediator) : base(name, mediator)
        {
        }
    }

    /// <summary>
    /// Utilisateur administrateur
    /// </summary>
    public class AdminUser : ChatUser
    {
        public AdminUser(string name, IChatMediator mediator) : base(name, mediator)
        {
        }

        public override void Receive(string message, ChatUser sender)
        {
            Console.WriteLine($"[ADMIN] {Name} a reçu de {sender.Name}: {message}");
        }
    }
}
