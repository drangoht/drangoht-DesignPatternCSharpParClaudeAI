using System;

namespace Patterns.Creational.FactoryMethod
{
    /// <summary>
    /// Test du pattern FactoryMethod
    /// </summary>
    public class FactoryMethodTest : Patterns.Creational.IPatternTest
    {
        public void Run()
        {
            Console.WriteLine("Démonstration du pattern FactoryMethod");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Le pattern Factory Method définit une interface pour créer un objet, mais laisse aux sous-classes le choix des classes à instancier.");
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
            // Cette méthode sera remplacée par le vrai code de test
            throw new NotImplementedException();
        }

        public string GetName()
        {
            return "FactoryMethod";
        }

        public string GetDescription()
        {
            return "Le pattern Factory Method définit une interface pour créer un objet, mais laisse aux sous-classes le choix des classes à instancier.";
        }
    }
}

