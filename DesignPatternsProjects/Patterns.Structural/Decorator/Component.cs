using System;

namespace Patterns.Structural.Decorator
{
    /// <summary>
    /// Interface de composant qui définit les opérations que les décorateurs peuvent modifier
    /// </summary>
    public interface IComponent
    {
        string Operation();
        double GetCost();
    }

    /// <summary>
    /// Composant concret qui implémente l'interface de base
    /// </summary>
    public class ConcreteComponent : IComponent
    {
        public string Operation()
        {
            return "Composant de base";
        }
        
        public double GetCost()
        {
            return 10.0;
        }
    }

    /// <summary>
    /// Décorateur de base qui suit la même interface que les autres composants
    /// </summary>
    public abstract class Decorator : IComponent
    {
        protected IComponent component;

        public Decorator(IComponent component)
        {
            this.component = component;
        }

        // Le décorateur délègue tout le travail au composant enveloppé par défaut
        public virtual string Operation()
        {
            return component.Operation();
        }
        
        public virtual double GetCost()
        {
            return component.GetCost();
        }
    }

    /// <summary>
    /// Décorateur concret qui ajoute un comportement avant et/ou après les appels aux méthodes du composant enveloppé
    /// </summary>
    public class ConcreteDecoratorA : Decorator
    {
        public ConcreteDecoratorA(IComponent component) : base(component)
        {
        }

        public override string Operation()
        {
            return $"ConcreteDecoratorA({base.Operation()})";
        }
        
        public override double GetCost()
        {
            return base.GetCost() + 5.0;
        }
    }

    /// <summary>
    /// Autre décorateur concret qui ajoute un comportement différent
    /// </summary>
    public class ConcreteDecoratorB : Decorator
    {
        public ConcreteDecoratorB(IComponent component) : base(component)
        {
        }

        public override string Operation()
        {
            return $"ConcreteDecoratorB({base.Operation()})";
        }
        
        public override double GetCost()
        {
            return base.GetCost() * 1.5;
        }
    }
}


