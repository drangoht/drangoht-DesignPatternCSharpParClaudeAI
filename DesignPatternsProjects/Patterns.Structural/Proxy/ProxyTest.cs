using System;

namespace Patterns.Structural.Proxy
{
    /// <summary>
    /// Test du pattern Proxy
    /// </summary>
    public class ProxyTest : Patterns.Structural.IPatternTest
    {
        public void Run()
        {
            Console.WriteLine("Démonstration du pattern Proxy");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Le pattern Proxy fournit un substitut ou un remplaçant pour un autre objet afin de contrôler l'accès à celui-ci.");
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
            Console.WriteLine("1. Test du Lazy Loading Proxy:");
            IImage image1 = new LazyImageProxy("photo1.jpg", 1024);
            Console.WriteLine("L'image n'est pas encore chargée...");
            image1.Display(); // L'image sera chargée ici
            Console.WriteLine("Deuxième affichage de la même image (déjà chargée):");
            image1.Display(); // Utilise l'image déjà chargée
            
            Console.WriteLine("\n2. Test du Protection Proxy:");
            IImage image2 = new ProtectedImageProxy("confidential_photo.jpg", 2048, "user");
            Console.WriteLine("Tentative d'accès avec le rôle 'user':");
            image2.Display(); // Accès refusé
            
            IImage image3 = new ProtectedImageProxy("confidential_photo.jpg", 2048, "admin");
            Console.WriteLine("\nTentative d'accès avec le rôle 'admin':");
            image3.Display(); // Accès autorisé
        }

        public string GetName()
        {
            return "Proxy";
        }

        public string GetDescription()
        {
            return "Le pattern Proxy fournit un substitut ou un remplaçant pour un autre objet afin de contrôler l'accès à celui-ci.";
        }
    }
}

