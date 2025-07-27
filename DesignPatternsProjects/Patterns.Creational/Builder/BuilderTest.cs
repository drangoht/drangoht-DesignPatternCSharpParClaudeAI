using System;

namespace Patterns.Creational.Builder
{
    /// <summary>
    /// Test du pattern Builder
    /// </summary>
    public class BuilderTest : Patterns.Creational.IPatternTest
    {
        public void Run()
        {
            Console.WriteLine("Démonstration du pattern Builder");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Le pattern Builder sépare la construction d'un objet complexe de sa représentation, permettant la création d'objets avec différentes configurations.");
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
            return "Builder";
        }

        public string GetDescription()
        {
            return "Le pattern Builder sépare la construction d'un objet complexe de sa représentation, permettant la création d'objets avec différentes configurations.";
        }
    }
}

