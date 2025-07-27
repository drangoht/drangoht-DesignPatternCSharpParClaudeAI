using System;
using System.Linq;
using Xunit;

namespace Patterns.Behavioral.Strategy.Tests
{
    public class StrategyTest
    {
        [Fact]
        public void TestStrategyPattern()
        {
            // Créer un tableau d'entiers non triés
            var numbers = new[] { 64, 34, 25, 12, 22, 11, 90 };
            Console.WriteLine($"Tableau initial: {string.Join(", ", numbers)}\n");

            // Test avec le tri à bulles
            var bubbleSort = new Sorter<int>(new BubbleSort<int>());
            var bubbleSorted = bubbleSort.Sort(numbers).ToArray();
            Console.WriteLine($"Résultat du tri à bulles: {string.Join(", ", bubbleSorted)}");
            Assert.Equal(new[] { 11, 12, 22, 25, 34, 64, 90 }, bubbleSorted);

            // Test avec le tri par sélection
            var selectionSort = new Sorter<int>(new SelectionSort<int>());
            var selectionSorted = selectionSort.Sort(numbers).ToArray();
            Console.WriteLine($"Résultat du tri par sélection: {string.Join(", ", selectionSorted)}");
            Assert.Equal(new[] { 11, 12, 22, 25, 34, 64, 90 }, selectionSorted);

            // Test avec le tri rapide
            var quickSort = new Sorter<int>(new QuickSort<int>());
            var quickSorted = quickSort.Sort(numbers).ToArray();
            Console.WriteLine($"Résultat du tri rapide: {string.Join(", ", quickSorted)}");
            Assert.Equal(new[] { 11, 12, 22, 25, 34, 64, 90 }, quickSorted);

            // Test de changement de stratégie
            var sorter = new Sorter<int>(new BubbleSort<int>());
            sorter.SetStrategy(new QuickSort<int>());
            var result = sorter.Sort(numbers).ToArray();
            Assert.Equal(new[] { 11, 12, 22, 25, 34, 64, 90 }, result);

            // Test avec une collection plus grande
            var random = new Random(42); // Seed fixe pour la reproductibilité
            var largeArray = Enumerable.Range(0, 1000)
                                     .Select(_ => random.Next(1000))
                                     .ToArray();

            Console.WriteLine("\nTest avec un grand tableau (1000 éléments):");
            
            // Mesure du temps pour chaque algorithme
            var strategies = new ISortStrategy<int>[]
            {
                new BubbleSort<int>(),
                new SelectionSort<int>(),
                new QuickSort<int>()
            };

            foreach (var strategy in strategies)
            {
                var startTime = DateTime.Now;
                sorter.SetStrategy(strategy);
                var sorted = sorter.Sort(largeArray).ToArray();
                var duration = DateTime.Now - startTime;
                
                Console.WriteLine($"Temps d'exécution pour {strategy.GetName()}: {duration.TotalMilliseconds}ms");
                Assert.Equal(largeArray.OrderBy(x => x), sorted);
            }
        }
    }
}
