using System;

namespace Patterns.Creational.Singleton
{
    /// <summary>
    /// Exemple de Singleton : Logger
    /// Utilisation : garantir une seule instance pour la gestion des logs.
    /// </summary>
    public class Logger
    {
        private static Logger? _instance;
        private static readonly object _lock = new object();

        // Constructeur privé pour empêcher l'instanciation externe
        private Logger() { }

        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new Logger();
                        }
                    }
                }
                return _instance;
            }
        }

        public void Log(string message)
        {
            Console.WriteLine($"[LOG] {message}");
        }
    }
}


