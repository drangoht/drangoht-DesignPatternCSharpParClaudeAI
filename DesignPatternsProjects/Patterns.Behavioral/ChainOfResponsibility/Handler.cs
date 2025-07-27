using System;

namespace Patterns.Behavioral.ChainOfResponsibility
{
    public class SupportTicket
    {
        public string Description { get; }
        public SupportLevel Level { get; }

        public SupportTicket(string description, SupportLevel level)
        {
            Description = description;
            Level = level;
        }
    }

    public enum SupportLevel
    {
        Basic,      // Niveau 1 - Problèmes simples
        Standard,   // Niveau 2 - Problèmes intermédiaires
        Critical,   // Niveau 3 - Problèmes critiques
        Emergency   // Problèmes urgents qui nécessitent une attention immédiate
    }

    public abstract class SupportHandler
    {
        private SupportHandler? _nextHandler;

        public SupportHandler SetNext(SupportHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public abstract bool ProcessTicket(SupportTicket ticket);

        protected bool PassToNext(SupportTicket ticket)
        {
            if (_nextHandler != null)
            {
                return _nextHandler.ProcessTicket(ticket);
            }

            Console.WriteLine($"Fin de la chaîne. Le ticket n'a pas pu être traité : {ticket.Description}");
            return false;
        }

        public void DisplayChain()
        {
            Console.WriteLine($"-> {this.GetType().Name}");
            if (_nextHandler != null)
            {
                _nextHandler.DisplayChain();
            }
        }
    }

    public class Level1Support : SupportHandler
    {
        public override bool ProcessTicket(SupportTicket ticket)
        {
            if (ticket.Level == SupportLevel.Basic)
            {
                Console.WriteLine($"Support niveau 1 traite le ticket : {ticket.Description}");
                return true;
            }

            Console.WriteLine($"Support niveau 1 transfère le ticket : {ticket.Description}");
            return PassToNext(ticket);
        }
    }

    public class Level2Support : SupportHandler
    {
        public override bool ProcessTicket(SupportTicket ticket)
        {
            if (ticket.Level == SupportLevel.Standard)
            {
                Console.WriteLine($"Support niveau 2 traite le ticket : {ticket.Description}");
                return true;
            }

            Console.WriteLine($"Support niveau 2 transfère le ticket : {ticket.Description}");
            return PassToNext(ticket);
        }
    }

    public class Level3Support : SupportHandler
    {
        public override bool ProcessTicket(SupportTicket ticket)
        {
            if (ticket.Level == SupportLevel.Critical)
            {
                Console.WriteLine($"Support niveau 3 traite le ticket : {ticket.Description}");
                return true;
            }

            Console.WriteLine($"Support niveau 3 transfère le ticket : {ticket.Description}");
            return PassToNext(ticket);
        }
    }

    public class EmergencySupport : SupportHandler
    {
        public override bool ProcessTicket(SupportTicket ticket)
        {
            if (ticket.Level == SupportLevel.Emergency)
            {
                Console.WriteLine($"Support d'urgence traite le ticket : {ticket.Description}");
                return true;
            }

            Console.WriteLine($"Support d'urgence ne peut pas traiter le ticket : {ticket.Description}");
            return PassToNext(ticket);
        }
    }
}
