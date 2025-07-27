using System;

namespace Patterns.Behavioral.TemplateMethod
{
    /// <summary>
    /// Abstract Class - Définit le template method qui contrôle le squelette de l'algorithme
    /// </summary>
    public abstract class BeveragePreparation
    {
        // Template Method - définit le squelette de l'algorithme
        public void PrepareBeverage()
        {
            BoilWater();
            Brew();
            PourInCup();
            if (CustomerWantsCondiments())
            {
                AddCondiments();
            }
            Console.WriteLine("Votre boisson est prête !\n");
        }

        // Opérations communes à toutes les boissons
        protected void BoilWater()
        {
            Console.WriteLine("🌡️ Eau en cours d'ébullition");
        }

        protected void PourInCup()
        {
            Console.WriteLine("🥤 Versement dans la tasse");
        }

        // Opérations qui doivent être implémentées par les sous-classes
        protected abstract void Brew();
        protected abstract void AddCondiments();

        // Hook - peut être surchargé par les sous-classes
        protected virtual bool CustomerWantsCondiments()
        {
            return true;
        }
    }

    /// <summary>
    /// Concrete Class - Prépare du café
    /// </summary>
    public class CoffeePreparation : BeveragePreparation
    {
        private readonly bool _wantsCondiments;

        public CoffeePreparation(bool wantsCondiments = true)
        {
            _wantsCondiments = wantsCondiments;
        }

        protected override void Brew()
        {
            Console.WriteLine("☕ Infusion du café");
        }

        protected override void AddCondiments()
        {
            Console.WriteLine("🥛 Ajout de lait et de sucre");
        }

        protected override bool CustomerWantsCondiments()
        {
            return _wantsCondiments;
        }
    }

    /// <summary>
    /// Concrete Class - Prépare du thé
    /// </summary>
    public class TeaPreparation : BeveragePreparation
    {
        private readonly bool _wantsCondiments;

        public TeaPreparation(bool wantsCondiments = true)
        {
            _wantsCondiments = wantsCondiments;
        }

        protected override void Brew()
        {
            Console.WriteLine("🫖 Infusion du thé");
        }

        protected override void AddCondiments()
        {
            Console.WriteLine("🍋 Ajout de citron");
        }

        protected override bool CustomerWantsCondiments()
        {
            return _wantsCondiments;
        }
    }
}
