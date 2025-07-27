using System;

namespace Patterns.Creational.AbstractFactory
{
    /// <summary>
    /// Classe client qui utilise la fabrique abstraite
    /// </summary>
    public class FurnitureClient
    {
        private readonly IChair _chair;
        private readonly ITable _table;

        /// <summary>
        /// Le client reçoit une fabrique abstraite et utilise ses méthodes pour créer des produits
        /// </summary>
        /// <param name="factory">Fabrique abstraite à utiliser</param>
        public FurnitureClient(IFurnitureFactory factory)
        {
            _chair = factory.CreateChair();
            _table = factory.CreateTable();
        }

        /// <summary>
        /// Méthode pour tester l'utilisation des meubles
        /// </summary>
        public void UseFurniture()
        {
            Console.WriteLine($"Style de l'ensemble: {_chair.GetStyle()}");
            _chair.SitOn();
            _table.PutOn("un livre");
        }
    }
}


