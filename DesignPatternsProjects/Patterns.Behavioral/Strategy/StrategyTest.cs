using System;

namespace Patterns.Behavioral.Strategy
{
    /// <summary>
    /// Test du pattern Strategy
    /// </summary>
    public class StrategyTest : Patterns.Behavioral.IPatternTest
    {
        public void Run()
        {
            Console.WriteLine("Démonstration du pattern Strategy");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Le pattern Strategy définit une famille d'algorithmes, les encapsule et les rend interchangeables.");
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
            // Créer un ensemble de données à trier
            List<int> data = new List<int> { 7, 2, 5, 1, 8, 9, 3, 6, 4 };
            
            Console.WriteLine("Données non triées: " + string.Join(", ", data));
            Console.WriteLine();

            // Créer le contexte avec une stratégie initiale
            Console.WriteLine("Initialisation avec la stratégie de tri à bulles...");
            SortContext context = new SortContext(new BubbleSortStrategy());
            
            // Exécuter la stratégie actuelle
            var result1 = context.ExecuteStrategy(data);
            Console.WriteLine("Résultat: " + string.Join(", ", result1));
            Console.WriteLine();
            
            // Changer de stratégie et réexécuter
            Console.WriteLine("Changement pour la stratégie de tri rapide...");
            context.SetStrategy(new QuickSortStrategy());
            var result2 = context.ExecuteStrategy(data);
            Console.WriteLine("Résultat: " + string.Join(", ", result2));
            Console.WriteLine();
            
            // Changer de stratégie et réexécuter
            Console.WriteLine("Changement pour la stratégie de tri par fusion...");
            context.SetStrategy(new MergeSortStrategy());
            var result3 = context.ExecuteStrategy(data);
            Console.WriteLine("Résultat: " + string.Join(", ", result3));
            Console.WriteLine();
            
            // Changer de stratégie et réexécuter
            Console.WriteLine("Changement pour la stratégie de tri LINQ...");
            context.SetStrategy(new LinqSortStrategy());
            var result4 = context.ExecuteStrategy(data);
            Console.WriteLine("Résultat: " + string.Join(", ", result4));
            
            Console.WriteLine("\nLe pattern Strategy permet de sélectionner l'algorithme à utiliser à l'exécution.");
            Console.WriteLine("Les algorithmes sont interchangeables et le code client reste le même.");
        }

        public string GetName()
        {
            return "Strategy";
        }

        public string GetDescription()
        {
            return "Le pattern Strategy définit une famille d'algorithmes, les encapsule et les rend interchangeables.";
        }
    }
}

