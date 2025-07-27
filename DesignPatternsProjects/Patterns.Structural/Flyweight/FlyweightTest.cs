using System;

namespace Patterns.Structural.Flyweight
{
    /// <summary>
    /// Test du pattern Flyweight
    /// </summary>
    public class FlyweightTest : Patterns.Structural.IPatternTest
    {
        public void Run()
        {
            Console.WriteLine("Démonstration du pattern Flyweight");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Le pattern Flyweight minimise l'utilisation de la mémoire en partageant autant que possible les objets similaires.");
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
            var factory = new TextFormatFactory();

            Console.WriteLine("1. Création et utilisation de styles de texte:");
            // Première utilisation de chaque style - création de nouveaux objets
            var heading1 = factory.GetTextFormat("Arial", 24, "blue");
            var heading2 = factory.GetTextFormat("Arial", 20, "red");
            var body = factory.GetTextFormat("Times New Roman", 12, "black");

            Console.WriteLine("\n2. Affichage de texte avec différents styles:");
            heading1.Display("Titre principal", "haut de page");
            heading2.Display("Sous-titre", "milieu de page");
            body.Display("Premier paragraphe", "corps du document");

            Console.WriteLine("\n3. Réutilisation des styles existants:");
            // Réutilisation des mêmes styles - pas de nouveaux objets créés
            var heading1Again = factory.GetTextFormat("Arial", 24, "blue");
            var bodyAgain = factory.GetTextFormat("Times New Roman", 12, "black");

            Console.WriteLine("\n4. Affichage avec les styles réutilisés:");
            heading1Again.Display("Second titre principal", "nouvelle page");
            bodyAgain.Display("Second paragraphe", "corps du document");

            Console.WriteLine($"\nNombre total de styles créés : {factory.GetFlyweightCount()}");
        }

        public string GetName()
        {
            return "Flyweight";
        }

        public string GetDescription()
        {
            return "Le pattern Flyweight minimise l'utilisation de la mémoire en partageant autant que possible les objets similaires.";
        }
    }
}

