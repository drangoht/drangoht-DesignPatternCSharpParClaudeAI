using System;

namespace Patterns.Structural.Facade
{
    /// <summary>
    /// Sous-système 1 - Gère l'authentification de l'utilisateur
    /// </summary>
    public class AuthenticationSystem
    {
        public bool Authenticate(string username, string password)
        {
            Console.WriteLine($"Authentification de l'utilisateur: {username}");
            // Logique simplifiée pour la démonstration
            return username.Length > 0 && password.Length > 0;
        }
    }

    /// <summary>
    /// Sous-système 2 - Gère l'autorisation d'accès
    /// </summary>
    public class AuthorizationSystem
    {
        public bool CheckAccess(string username, string resource)
        {
            Console.WriteLine($"Vérification des droits d'accès pour l'utilisateur {username} sur la ressource {resource}");
            // Logique simplifiée pour la démonstration
            return true;
        }
    }

    /// <summary>
    /// Sous-système 3 - Gère la journalisation des activités
    /// </summary>
    public class LoggingSystem
    {
        public void LogAccess(string username, string resource, bool accessGranted)
        {
            Console.WriteLine($"Journalisation: utilisateur {username}, ressource {resource}, accès {(accessGranted ? "accordé" : "refusé")}");
        }
    }

    /// <summary>
    /// Sous-système 4 - Gère la notification des activités
    /// </summary>
    public class NotificationSystem
    {
        public void NotifyAdmin(string username, string resource, bool accessGranted)
        {
            if (!accessGranted)
            {
                Console.WriteLine($"ALERTE: Tentative d'accès refusée pour {username} sur {resource}");
            }
        }
    }
}


