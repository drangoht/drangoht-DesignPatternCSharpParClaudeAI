using System;
using System.Collections.Generic;
using System.Linq;

namespace Patterns.Behavioral.Strategy
{
    /// <summary>
    /// Strategy - Interface pour les stratégies de tri
    /// </summary>
    public interface ISortStrategy<T>
    {
        IEnumerable<T> Sort(IEnumerable<T> items);
        string GetName();
        string GetComplexity();
        string GetDescription();
    }

    /// <summary>
    /// Concrete Strategy - Tri à bulles
    /// </summary>
    public class BubbleSort<T> : ISortStrategy<T> where T : IComparable<T>
    {
        public IEnumerable<T> Sort(IEnumerable<T> items)
        {
            var array = items.ToArray();
            var n = array.Length;
            
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (array[j].CompareTo(array[j + 1]) > 0)
                    {
                        // Échange
                        var temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }

            return array;
        }

        public string GetName() => "Tri à bulles";

        public string GetComplexity() => "O(n²)";

        public string GetDescription() =>
            "Compare et échange les éléments adjacents. Simple mais inefficace pour les grandes collections.";
    }

    /// <summary>
    /// Concrete Strategy - Tri par sélection
    /// </summary>
    public class SelectionSort<T> : ISortStrategy<T> where T : IComparable<T>
    {
        public IEnumerable<T> Sort(IEnumerable<T> items)
        {
            var array = items.ToArray();
            var n = array.Length;

            for (int i = 0; i < n - 1; i++)
            {
                var minIdx = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (array[j].CompareTo(array[minIdx]) < 0)
                    {
                        minIdx = j;
                    }
                }

                if (minIdx != i)
                {
                    var temp = array[i];
                    array[i] = array[minIdx];
                    array[minIdx] = temp;
                }
            }

            return array;
        }

        public string GetName() => "Tri par sélection";

        public string GetComplexity() => "O(n²)";

        public string GetDescription() =>
            "Trouve le plus petit élément et le place au début. Plus efficace que le tri à bulles en termes d'échanges.";
    }

    /// <summary>
    /// Concrete Strategy - Tri rapide (QuickSort)
    /// </summary>
    public class QuickSort<T> : ISortStrategy<T> where T : IComparable<T>
    {
        public IEnumerable<T> Sort(IEnumerable<T> items)
        {
            var array = items.ToArray();
            QuickSortInternal(array, 0, array.Length - 1);
            return array;
        }

        private void QuickSortInternal(T[] array, int low, int high)
        {
            if (low < high)
            {
                var pivotIndex = Partition(array, low, high);
                QuickSortInternal(array, low, pivotIndex - 1);
                QuickSortInternal(array, pivotIndex + 1, high);
            }
        }

        private int Partition(T[] array, int low, int high)
        {
            var pivot = array[high];
            var i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (array[j].CompareTo(pivot) <= 0)
                {
                    i++;
                    var temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }

            var temp2 = array[i + 1];
            array[i + 1] = array[high];
            array[high] = temp2;

            return i + 1;
        }

        public string GetName() => "Tri rapide (QuickSort)";

        public string GetComplexity() => "O(n log n) en moyenne, O(n²) dans le pire cas";

        public string GetDescription() =>
            "Divise pour régner avec un pivot. Très efficace en pratique et largement utilisé.";
    }

    /// <summary>
    /// Context - Classe qui utilise la stratégie de tri
    /// </summary>
    public class Sorter<T> where T : IComparable<T>
    {
        private ISortStrategy<T> _strategy;

        public Sorter(ISortStrategy<T> strategy)
        {
            _strategy = strategy;
        }

        public void SetStrategy(ISortStrategy<T> strategy)
        {
            _strategy = strategy;
        }

        public IEnumerable<T> Sort(IEnumerable<T> items)
        {
            Console.WriteLine($"\nUtilisation de la stratégie: {_strategy.GetName()}");
            Console.WriteLine($"Complexité: {_strategy.GetComplexity()}");
            Console.WriteLine($"Description: {_strategy.GetDescription()}\n");

            return _strategy.Sort(items);
        }
    }
}
