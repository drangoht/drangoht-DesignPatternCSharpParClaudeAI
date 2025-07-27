using System;

namespace Patterns.Creational.Singleton
{
    /// <summary>
    /// Test du Singleton Logger
    /// </summary>
    public class LoggerTest : Patterns.Creational.IPatternTest
    {
        public void Run()
        {
            Logger logger1 = Logger.Instance;
            Logger logger2 = Logger.Instance;

            logger1.Log("Premier message");
            logger2.Log("Second message");

            // Vérification que les deux instances sont identiques
            Console.WriteLine($"logger1 == logger2 ? {object.ReferenceEquals(logger1, logger2)}");
        }

        public string GetName()
        {
            return "Singleton";
        }

        public string GetDescription()
        {
            return "Le pattern Singleton garantit qu'une classe n'a qu'une seule instance et fournit un point d'accès global à cette instance.";
        }
    }
}

