using System;

namespace Patterns.Structural.Decorator
{
    /// <summary>
    /// Test du pattern Decorator
    /// </summary>
    public class DecoratorTest : Patterns.Structural.IPatternTest
    {
        public void Run()
        {
            Console.WriteLine("Démonstration du pattern Decorator");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Le pattern Decorator attache dynamiquement des responsabilités supplémentaires à un objet.");
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
            // Créer un composant simple
            Console.WriteLine("1. Création d'un composant de base");
            IComponent simpleComponent = new ConcreteComponent();
            Console.WriteLine($"Résultat: {simpleComponent.Operation()}");
            Console.WriteLine($"Coût: {simpleComponent.GetCost():C2}");
            
            Console.WriteLine("\n2. Ajout du décorateur A au composant de base");
            IComponent decoratedWithA = new ConcreteDecoratorA(simpleComponent);
            Console.WriteLine($"Résultat: {decoratedWithA.Operation()}");
            Console.WriteLine($"Coût: {decoratedWithA.GetCost():C2}");
            
            Console.WriteLine("\n3. Ajout du décorateur B au composant de base");
            IComponent decoratedWithB = new ConcreteDecoratorB(simpleComponent);
            Console.WriteLine($"Résultat: {decoratedWithB.Operation()}");
            Console.WriteLine($"Coût: {decoratedWithB.GetCost():C2}");
            
            Console.WriteLine("\n4. Composition de plusieurs décorateurs (A + B)");
            IComponent decoratedWithAB = new ConcreteDecoratorB(new ConcreteDecoratorA(simpleComponent));
            Console.WriteLine($"Résultat: {decoratedWithAB.Operation()}");
            Console.WriteLine($"Coût: {decoratedWithAB.GetCost():C2}");
            
            Console.WriteLine("\n5. Composition alternative (B + A)");
            IComponent decoratedWithBA = new ConcreteDecoratorA(new ConcreteDecoratorB(simpleComponent));
            Console.WriteLine($"Résultat: {decoratedWithBA.Operation()}");
            Console.WriteLine($"Coût: {decoratedWithBA.GetCost():C2}");
            
            Console.WriteLine("\nLe pattern Decorator nous permet d'ajouter des fonctionnalités dynamiquement");
            Console.WriteLine("sans modifier le code existant, en respectant le principe de responsabilité unique.");
        }

        public string GetName()
        {
            return "Decorator";
        }

        public string GetDescription()
        {
            return "Le pattern Decorator attache dynamiquement des responsabilités supplémentaires à un objet.";
        }
    }
}

