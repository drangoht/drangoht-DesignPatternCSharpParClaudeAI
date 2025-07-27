using System;
using System.Collections.Generic;
using System.Linq;

namespace Patterns.Behavioral.Strategy
{
    /// <summary>
    /// Interface de stratégie qui définit la méthode commune à toutes les stratégies
    /// </summary>
    public interface ISortStrategy
    {
        List<int> Sort(List<int> list);
        string Name { get; }
    }

    /// <summary>
    /// Stratégie concrète utilisant le tri à bulles
    /// </summary>
    public class BubbleSortStrategy : ISortStrategy
    {
        public string Name => "Tri à bulles";

        public List<int> Sort(List<int> list)
        {
            Console.WriteLine("Tri à bulles en cours...");
            var result = new List<int>(list);
            int n = result.Count;
            
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (result[j] > result[j + 1])
                    {
                        // Échange des éléments
                        var temp = result[j];
                        result[j] = result[j + 1];
                        result[j + 1] = temp;
                    }
                }
            }
            
            return result;
        }
    }

    /// <summary>
    /// Stratégie concrète utilisant le tri rapide
    /// </summary>
    public class QuickSortStrategy : ISortStrategy
    {
        public string Name => "Tri rapide";

        public List<int> Sort(List<int> list)
        {
            Console.WriteLine("Tri rapide en cours...");
            var result = new List<int>(list);
            QuickSort(result, 0, result.Count - 1);
            return result;
        }

        private void QuickSort(List<int> array, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(array, left, right);
                QuickSort(array, left, pivot - 1);
                QuickSort(array, pivot + 1, right);
            }
        }

        private int Partition(List<int> array, int left, int right)
        {
            int pivot = array[right];
            int i = left - 1;
            
            for (int j = left; j < right; j++)
            {
                if (array[j] <= pivot)
                {
                    i++;
                    var temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
            
            var temp2 = array[i + 1];
            array[i + 1] = array[right];
            array[right] = temp2;
            
            return i + 1;
        }
    }

    /// <summary>
    /// Stratégie concrète utilisant le tri par fusion
    /// </summary>
    public class MergeSortStrategy : ISortStrategy
    {
        public string Name => "Tri par fusion";

        public List<int> Sort(List<int> list)
        {
            Console.WriteLine("Tri par fusion en cours...");
            var result = new List<int>(list);
            
            if (result.Count <= 1)
                return result;
                
            return MergeSort(result);
        }

        private List<int> MergeSort(List<int> list)
        {
            if (list.Count <= 1)
                return list;
                
            int middle = list.Count / 2;
            var left = new List<int>();
            var right = new List<int>();
            
            for (int i = 0; i < middle; i++)
                left.Add(list[i]);
                
            for (int i = middle; i < list.Count; i++)
                right.Add(list[i]);
                
            left = MergeSort(left);
            right = MergeSort(right);
            
            return Merge(left, right);
        }

        private List<int> Merge(List<int> left, List<int> right)
        {
            var result = new List<int>();
            
            while (left.Count > 0 && right.Count > 0)
            {
                if (left[0] <= right[0])
                {
                    result.Add(left[0]);
                    left.RemoveAt(0);
                }
                else
                {
                    result.Add(right[0]);
                    right.RemoveAt(0);
                }
            }
            
            while (left.Count > 0)
            {
                result.Add(left[0]);
                left.RemoveAt(0);
            }
            
            while (right.Count > 0)
            {
                result.Add(right[0]);
                right.RemoveAt(0);
            }
            
            return result;
        }
    }

    /// <summary>
    /// Stratégie concrète utilisant LINQ pour trier
    /// </summary>
    public class LinqSortStrategy : ISortStrategy
    {
        public string Name => "Tri LINQ";

        public List<int> Sort(List<int> list)
        {
            Console.WriteLine("Tri LINQ en cours...");
            return list.OrderBy(x => x).ToList();
        }
    }

    /// <summary>
    /// Contexte qui utilise une stratégie
    /// </summary>
    public class SortContext
    {
        private ISortStrategy _strategy;

        public SortContext(ISortStrategy strategy)
        {
            _strategy = strategy;
        }

        public void SetStrategy(ISortStrategy strategy)
        {
            _strategy = strategy;
            Console.WriteLine($"Stratégie changée pour: {strategy.Name}");
        }

        public List<int> ExecuteStrategy(List<int> data)
        {
            return _strategy.Sort(data);
        }
    }
}
