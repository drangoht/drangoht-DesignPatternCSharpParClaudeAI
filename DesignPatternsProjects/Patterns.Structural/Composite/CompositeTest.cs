using System;

namespace Patterns.Structural.Composite
{
    /// <summary>
    /// Test du pattern Composite
    /// </summary>
    public class CompositeTest : Patterns.Structural.IPatternTest
    {
        public void Run()
        {
            Console.WriteLine("Démonstration du pattern Composite");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Le pattern Composite compose des objets en structures arborescentes pour représenter des hiérarchies partie-tout.");
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
            // Créer des produits individuels (leaf)
            Product phone = new Product("Smartphone", 799.99);
            Product headphones = new Product("Casque audio", 199.99);
            Product charger = new Product("Chargeur", 29.99);
            Product case_ = new Product("Étui de protection", 19.99);

            // Créer une petite boîte pour les accessoires
            Box accessoryBox = new Box("Boîte d'accessoires");
            accessoryBox.Add(headphones);
            accessoryBox.Add(charger);
            accessoryBox.Add(case_);

            // Créer la boîte principale
            Box mainBox = new Box("Boîte principale (Pack smartphone)");
            mainBox.Add(phone);
            mainBox.Add(accessoryBox);

            // Afficher le contenu et le prix total
            Console.WriteLine("Structure de l'emballage:");
            mainBox.Display();
            
            Console.WriteLine($"\nPrix total du pack: €{mainBox.GetPrice():F2}");
            Console.WriteLine($"Prix des accessoires uniquement: €{accessoryBox.GetPrice():F2}");
            
            // Démontrer l'utilisation du pattern
            Console.WriteLine("\nDémonstration de la manipulation de la structure:");
            Console.WriteLine("Ajout d'un produit supplémentaire...");
            
            Product screenProtector = new Product("Protection d'écran", 9.99);
            accessoryBox.Add(screenProtector);
            
            Console.WriteLine("\nNouvelle structure après modification:");
            mainBox.Display();
            
            Console.WriteLine($"\nNouveau prix total: €{mainBox.GetPrice():F2}");
        }

        public string GetName()
        {
            return "Composite";
        }

        public string GetDescription()
        {
            return "Le pattern Composite compose des objets en structures arborescentes pour représenter des hiérarchies partie-tout.";
        }
    }
}

