using System;

namespace Patterns.Creational.AbstractFactory
{
    /// <summary>
    /// Test du pattern AbstractFactory
    /// </summary>
    public class AbstractFactoryTest : Patterns.Creational.IPatternTest
    {
        public void Run()
        {
            Console.WriteLine("Démonstration du pattern AbstractFactory");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Le pattern Abstract Factory permet de créer des familles d'objets liés sans spécifier leurs classes concrètes.");
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
            Console.WriteLine("1. Création d'un ensemble de mobilier de style moderne :");
            var modernFactory = new ModernFurnitureFactory();
            var modernClient = new FurnitureClient(modernFactory);
            modernClient.UseFurniture();
            
            Console.WriteLine("\n2. Création d'un ensemble de mobilier de style victorien :");
            var victorianFactory = new VictorianFurnitureFactory();
            var victorianClient = new FurnitureClient(victorianFactory);
            victorianClient.UseFurniture();

            Console.WriteLine("\nAvantages du pattern Abstract Factory :");
            Console.WriteLine("- Garantit la compatibilité entre les produits créés");
            Console.WriteLine("- Isole le code client des classes concrètes de produits");
            Console.WriteLine("- Facilite l'échange de familles de produits");
            Console.WriteLine("- Encourage le respect du principe de responsabilité unique");
            Console.WriteLine("- Implémente le principe ouvert/fermé: ajout facile de nouvelles variantes");
        }

        public string GetName()
        {
            return "AbstractFactory";
        }

        public string GetDescription()
        {
            return "Le pattern Abstract Factory permet de créer des familles d'objets liés sans spécifier leurs classes concrètes.";
        }
    }
}

