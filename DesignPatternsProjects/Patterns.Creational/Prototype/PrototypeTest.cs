using System;

namespace Patterns.Creational.Prototype
{
    /// <summary>
    /// Test du pattern Prototype
    /// </summary>
    public class PrototypeTest : Patterns.Creational.IPatternTest
    {
        public void Run()
        {
            Console.WriteLine("Démonstration du pattern Prototype");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Le pattern Prototype permet de créer de nouveaux objets en copiant des objets existants, évitant ainsi de dépendre de leurs classes.");
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
            return "Prototype";
        }

        public string GetDescription()
        {
            return "Le pattern Prototype permet de créer de nouveaux objets en copiant des objets existants, évitant ainsi de dépendre de leurs classes.";
        }
    }
}

