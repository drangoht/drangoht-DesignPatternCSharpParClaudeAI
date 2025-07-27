using System;

namespace Patterns.Behavioral.Iterator
{
    /// <summary>
    /// Test du pattern Iterator
    /// </summary>
    public class IteratorTest : IPatternTest
    {
        public void Run()
        {
            Console.WriteLine("Démonstration du pattern Iterator");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Le pattern Iterator permet de parcourir les éléments d'une collection sans exposer sa structure interne.");
            Console.WriteLine();
            
            // Code de démonstration du pattern
            Console.WriteLine("Exemple du pattern en action:");
            try 
            {
                RunPatternDemo();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'exécution: {ex.Message}");
            }
        }

        private void RunPatternDemo()
        {
            // Création d'un arbre binaire
            var tree = new BinaryTree<int>();
            foreach (var value in new[] { 1, 2, 3, 4, 5, 6, 7 })
            {
                tree.Add(value);
            }

            Console.WriteLine("Arbre binaire avec les valeurs: 1, 2, 3, 4, 5, 6, 7");
            Console.WriteLine("Structure de l'arbre:");
            Console.WriteLine("       1");
            Console.WriteLine("     /   \\");
            Console.WriteLine("    2     3");
            Console.WriteLine("   / \\   / \\");
            Console.WriteLine("  4   5 6   7");
            Console.WriteLine();

            // Démonstration du parcours préfixe (PreOrder)
            Console.WriteLine("1. Parcours préfixe (PreOrder: Racine-Gauche-Droite):");
            Console.WriteLine("   Ce parcours visite d'abord la racine, puis le sous-arbre gauche, et enfin le sous-arbre droit");
            var preOrderIterator = tree.GetIterator(TraversalStrategy.PreOrder);
            PrintTraversal(preOrderIterator);  // Résultat attendu: 1, 2, 4, 5, 3, 6, 7

            // Démonstration du parcours infixe (InOrder)
            Console.WriteLine("\n2. Parcours infixe (InOrder: Gauche-Racine-Droite):");
            Console.WriteLine("   Ce parcours visite d'abord le sous-arbre gauche, puis la racine, et enfin le sous-arbre droit");
            Console.WriteLine("   Pour un arbre binaire de recherche, ce parcours donne les éléments dans l'ordre croissant");
            var inOrderIterator = tree.GetIterator(TraversalStrategy.InOrder);
            PrintTraversal(inOrderIterator);   // Résultat attendu: 4, 2, 5, 1, 6, 3, 7

            // Démonstration du parcours postfixe (PostOrder)
            Console.WriteLine("\n3. Parcours postfixe (PostOrder: Gauche-Droite-Racine):");
            Console.WriteLine("   Ce parcours visite d'abord les sous-arbres gauche et droit, puis la racine");
            Console.WriteLine("   Utile pour supprimer un arbre ou évaluer une expression mathématique en notation postfixe");
            var postOrderIterator = tree.GetIterator(TraversalStrategy.PostOrder);
            PrintTraversal(postOrderIterator); // Résultat attendu: 4, 5, 2, 6, 7, 3, 1

            // Démonstration du parcours par niveau (LevelOrder)
            Console.WriteLine("\n4. Parcours par niveau (LevelOrder):");
            Console.WriteLine("   Ce parcours visite les nœuds niveau par niveau, de gauche à droite");
            Console.WriteLine("   Utile pour visualiser la structure de l'arbre ou trouver le chemin le plus court");
            var levelOrderIterator = tree.GetIterator(TraversalStrategy.LevelOrder);
            PrintTraversal(levelOrderIterator); // Résultat attendu: 1, 2, 3, 4, 5, 6, 7

            // Démonstration de l'utilisation avec foreach
            Console.WriteLine("\n5. Utilisation avec foreach (stratégie par défaut: InOrder):");
            Console.WriteLine("   Le pattern Iterator permet d'utiliser la syntaxe foreach sur notre collection");
            foreach (var value in tree)
            {
                Console.Write($"{value} ");
            }
            Console.WriteLine("\n");

            // Démonstration avec un arbre plus complexe
            Console.WriteLine("6. Exemple avec un arbre plus complexe:");
            var complexTree = CreateComplexTree();
            Console.WriteLine("Structure de l'arbre complexe:");
            Console.WriteLine("         10");
            Console.WriteLine("       /    \\");
            Console.WriteLine("      5      15");
            Console.WriteLine("    /  \\    /  \\");
            Console.WriteLine("   3    7  12   18");
            Console.WriteLine("  /    /     \\");
            Console.WriteLine(" 1    6      13");
            Console.WriteLine();

            foreach (var strategy in Enum.GetValues<TraversalStrategy>())
            {
                Console.WriteLine($"Parcours {strategy}:");
                var iterator = complexTree.GetIterator(strategy);
                PrintTraversal(iterator);
            }
        }

        private void PrintTraversal(ITreeIterator<int> iterator)
        {
            Console.Write("   Résultat: ");
            while (iterator.MoveNext())
            {
                Console.Write($"{iterator.Current} ");
            }
            Console.WriteLine();
        }

        private BinaryTree<int> CreateComplexTree()
        {
            var tree = new BinaryTree<int>();
            foreach (var value in new[] { 10, 5, 15, 3, 7, 12, 18, 1, 6, 13 })
            {
                tree.Add(value);
            }
            return tree;
        }

        public string GetName()
        {
            return "Iterator";
        }

        public string GetDescription()
        {
            return "Le pattern Iterator permet de parcourir les éléments d'une collection sans exposer sa structure interne.";
        }
    }
}
