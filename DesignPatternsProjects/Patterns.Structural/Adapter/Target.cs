using System;

namespace Patterns.Structural.Adapter
{
    /// <summary>
    /// Interface cible que le client utilise
    /// </summary>
    public interface ITarget
    {
        string GetRequest();
    }

    /// <summary>
    /// Classe adaptée (qui doit être adaptée)
    /// Système existant ou externe avec une interface incompatible.
    /// </summary>
    public class Adaptee
    {
        /// <summary>
        /// Méthode avec une signature différente de celle attendue par le client
        /// </summary>
        public string GetSpecificRequest()
        {
            return "Requête spécifique de l'Adaptee";
        }
    }

    /// <summary>
    /// Adaptateur qui convertit l'interface de l'Adaptee en l'interface ITarget.
    /// </summary>
    public class Adapter : ITarget
    {
        private readonly Adaptee _adaptee;

        public Adapter(Adaptee adaptee)
        {
            _adaptee = adaptee;
        }

        /// <summary>
        /// Implémentation de la méthode de l'interface cible qui fait appel à la méthode de l'Adaptee
        /// </summary>
        public string GetRequest()
        {
            // Adaptation de l'appel à la méthode de l'Adaptee
            return $"ADAPTÉ: {_adaptee.GetSpecificRequest()}";
        }
    }
}


