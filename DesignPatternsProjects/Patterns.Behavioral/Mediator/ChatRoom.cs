using System;
using System.Collections.Generic;
using System.Linq;

namespace Patterns.Behavioral.Mediator
{
    /// <summary>
    /// Médiateur - Interface pour la salle de chat
    /// </summary>
    public interface IChatMediator
    {
        void Register(ChatUser user);
        void Send(string message, ChatUser sender);
        void SendTo(string message, ChatUser sender, ChatUser receiver);
        void SendPrivate(string message, ChatUser sender, ChatUser receiver);
        void Broadcast(string announcement, ChatUser sender);
    }

    /// <summary>
    /// Médiateur concret - Implémentation de la salle de chat
    /// </summary>
    public class ChatRoom : IChatMediator
    {
        private readonly List<ChatUser> _users;
        private readonly string _name;

        public ChatRoom(string name)
        {
            _name = name;
            _users = new List<ChatUser>();
            Console.WriteLine($"Salon de discussion '{_name}' créé.");
        }

        public void Register(ChatUser user)
        {
            if (!_users.Contains(user))
            {
                _users.Add(user);
                Console.WriteLine($"{user.Name} a rejoint le salon {_name}.");
            }
        }

        public void Send(string message, ChatUser sender)
        {
            Console.WriteLine($"\n[{_name}] {sender.Name}: {message}");
            foreach (var user in _users.Where(u => u != sender))
            {
                user.Receive(message, sender);
            }
        }

        public void SendTo(string message, ChatUser sender, ChatUser receiver)
        {
            if (_users.Contains(receiver))
            {
                Console.WriteLine($"\n[{_name}] {sender.Name} à {receiver.Name}: {message}");
                receiver.Receive(message, sender);
            }
            else
            {
                Console.WriteLine($"\n[{_name}] Erreur: L'utilisateur {receiver.Name} n'est pas dans le salon.");
            }
        }

        public void SendPrivate(string message, ChatUser sender, ChatUser receiver)
        {
            if (_users.Contains(receiver))
            {
                Console.WriteLine($"\n[{_name}] (Message privé) {sender.Name} à {receiver.Name}: {message}");
                receiver.ReceivePrivate(message, sender);
            }
            else
            {
                Console.WriteLine($"\n[{_name}] Erreur: L'utilisateur {receiver.Name} n'est pas dans le salon.");
            }
        }

        public void Broadcast(string announcement, ChatUser sender)
        {
            if (sender is ModeratorUser || sender is AdminUser)
            {
                Console.WriteLine($"\n[{_name}] ANNONCE de {sender.Name}: {announcement}");
                foreach (var user in _users.Where(u => u != sender))
                {
                    user.ReceiveAnnouncement(announcement, sender);
                }
            }
            else
            {
                Console.WriteLine($"\n[{_name}] Erreur: {sender.Name} n'a pas les droits pour faire une annonce.");
            }
        }
    }
}
