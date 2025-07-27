using System;
using System.Collections.Generic;

namespace Patterns.Structural.Composite
{
    /// <summary>
    /// Composant abstrait qui déclare l'interface commune pour les objets simples et composés
    /// </summary>
    public abstract class Component
    {
        public string Name { get; protected set; }
        
        // Peut avoir un parent pour faciliter la navigation dans l'arborescence
        protected Component parent;

        public Component(string name)
        {
            this.Name = name;
        }

        public void SetParent(Component parent)
        {
            this.parent = parent;
        }

        public Component GetParent()
        {
            return parent;
        }

        // Opérations de base pour manipuler les enfants. Par défaut, ces méthodes
        // lèvent une exception pour les composants leaf.
        public virtual void Add(Component component)
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(Component component)
        {
            throw new NotImplementedException();
        }

        // La méthode qui détermine si un composant est un composite
        public virtual bool IsComposite()
        {
            return false;
        }

        // L'opération principale que tous les composants doivent implémenter
        public abstract double GetPrice();
        
        // Méthode pour afficher les informations du composant
        public virtual void Display(int depth = 0)
        {
            Console.WriteLine($"{new string('-', depth)} {Name} (€{GetPrice()})");
        }
    }

    /// <summary>
    /// Classe Leaf (feuille) qui représente les objets finaux d'une composition.
    /// Un objet leaf ne peut pas avoir d'enfants.
    /// </summary>
    public class Product : Component
    {
        private double price;

        public Product(string name, double price) : base(name)
        {
            this.price = price;
        }

        // Retourne simplement le prix du produit
        public override double GetPrice()
        {
            return price;
        }
    }

    /// <summary>
    /// Classe Composite qui représente les composants complexes qui peuvent avoir des enfants.
    /// Les composites délèguent le travail à leurs enfants et agrègent les résultats.
    /// </summary>
    public class Box : Component
    {
        protected List<Component> children = new List<Component>();

        public Box(string name) : base(name)
        {
        }

        // Un composite peut ajouter ou supprimer d'autres composants (simples ou composés)
        public override void Add(Component component)
        {
            children.Add(component);
            component.SetParent(this);
        }

        public override void Remove(Component component)
        {
            children.Remove(component);
            component.SetParent(null);
        }

        public override bool IsComposite()
        {
            return true;
        }

        // Un composite délègue le travail à ses enfants et agrège les résultats
        public override double GetPrice()
        {
            double total = 0;
            foreach (var child in children)
            {
                total += child.GetPrice();
            }
            return total;
        }
        
        // Affiche l'arborescence avec indentation
        public override void Display(int depth = 0)
        {
            Console.WriteLine($"{new string('-', depth)} {Name} (€{GetPrice()})");
            
            foreach (var child in children)
            {
                child.Display(depth + 2);
            }
        }
    }
}


