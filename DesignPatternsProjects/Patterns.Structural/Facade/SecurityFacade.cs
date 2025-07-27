using System;

namespace Patterns.Structural.Facade
{
    /// <summary>
    /// Façade - Fournit une interface simplifiée pour l'ensemble du système de sécurité
    /// Le pattern Facade offre une interface unifiée à un ensemble d'interfaces dans un sous-système.
    /// Il définit une interface de haut niveau qui rend le sous-système plus facile à utiliser.
    /// </summary>
    public class SecurityFacade
    {
        private readonly AuthenticationSystem _authenticationSystem;
        private readonly AuthorizationSystem _authorizationSystem;
        private readonly LoggingSystem _loggingSystem;
        private readonly NotificationSystem _notificationSystem;

        public SecurityFacade()
        {
            _authenticationSystem = new AuthenticationSystem();
            _authorizationSystem = new AuthorizationSystem();
            _loggingSystem = new LoggingSystem();
            _notificationSystem = new NotificationSystem();
        }

        /// <summary>
        /// Méthode de la façade qui simplifie le processus complet de sécurité
        /// </summary>
        /// <param name="username">Nom d'utilisateur</param>
        /// <param name="password">Mot de passe</param>
        /// <param name="resource">Ressource demandée</param>
        /// <returns>Résultat de l'accès</returns>
        public bool CheckAccess(string username, string password, string resource)
        {
            Console.WriteLine("\nDémarrage du processus de vérification d'accès via la façade...");
            
            bool isAuthenticated = _authenticationSystem.Authenticate(username, password);
            
            if (!isAuthenticated)
            {
                _loggingSystem.LogAccess(username, resource, false);
                _notificationSystem.NotifyAdmin(username, resource, false);
                return false;
            }
            
            bool isAuthorized = _authorizationSystem.CheckAccess(username, resource);
            
            _loggingSystem.LogAccess(username, resource, isAuthorized);
            _notificationSystem.NotifyAdmin(username, resource, isAuthorized);
            
            return isAuthorized;
        }
    }
}


