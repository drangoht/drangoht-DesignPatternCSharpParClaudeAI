using System;

namespace Patterns.Creational.Singleton
{
    /// <summary>
    /// Test du pattern Singleton
    /// </summary>
    public class SingletonTest : Patterns.Creational.IPatternTest
    {
        public void Run()
        {
            Console.WriteLine("Démonstration du pattern Singleton");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Le pattern Singleton garantit qu'une classe n'a qu'une seule instance et fournit un point d'accès global à cette instance.");
            Console.WriteLine();
            
            // Code de démonstration du pattern
            Console.WriteLine("Exemple du pattern en action:");
            try 
            {
                // Exécuter le code du pattern
                RunPatternDemo();
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("La démonstration complète n'est pas encore implémentée.");
                Console.WriteLine("Consultez le code source pour plus de détails sur ce pattern.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'exécution: {ex.Message}");
            }
        }

        private void RunPatternDemo()
        {
            Console.WriteLine("1. Obtention de l'instance du Logger :");
            var logger1 = Logger.Instance;
            logger1.Log("Premier message de log");

            Console.WriteLine("\n2. Obtention d'une deuxième référence au Logger :");
            var logger2 = Logger.Instance;
            logger2.Log("Deuxième message de log");

            Console.WriteLine("\n3. Vérification que les deux références pointent vers la même instance :");
            Console.WriteLine($"logger1 et logger2 sont la même instance : {ReferenceEquals(logger1, logger2)}");
            Console.WriteLine($"HashCode logger1 : {logger1.GetHashCode()}");
            Console.WriteLine($"HashCode logger2 : {logger2.GetHashCode()}");

            Console.WriteLine("\n4. Test du comportement thread-safe :");
            var tasks = new System.Threading.Tasks.Task[5];
            for (int i = 0; i < 5; i++)
            {
                tasks[i] = System.Threading.Tasks.Task.Run(() =>
                {
                    var logger = Logger.Instance;
                    logger.Log($"Message de log du thread {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                });
            }
            System.Threading.Tasks.Task.WaitAll(tasks);

            Console.WriteLine("\nAvantages du pattern Singleton :");
            Console.WriteLine("- Garantit une instance unique");
            Console.WriteLine("- Point d'accès global à cette instance");
            Console.WriteLine("- L'instance n'est créée qu'à la première utilisation");
            Console.WriteLine("- Permet de contrôler l'accès concurrent (thread-safe)");
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

