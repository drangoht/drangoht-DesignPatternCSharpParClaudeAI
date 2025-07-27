using System;

namespace Patterns.Creational.AbstractFactory
{
    /// <summary>
    /// Interface de fabrique abstraite qui définit les méthodes de création
    /// </summary>
    public interface IFurnitureFactory
    {
        /// <summary>
        /// Crée une chaise
        /// </summary>
        IChair CreateChair();

        /// <summary>
        /// Crée une table
        /// </summary>
        ITable CreateTable();
    }

    /// <summary>
    /// Fabrique concrète pour créer des meubles de style moderne
    /// </summary>
    public class ModernFurnitureFactory : IFurnitureFactory
    {
        public IChair CreateChair()
        {
            return new ModernChair();
        }

        public ITable CreateTable()
        {
            return new ModernTable();
        }
    }

    /// <summary>
    /// Fabrique concrète pour créer des meubles de style victorien
    /// </summary>
    public class VictorianFurnitureFactory : IFurnitureFactory
    {
        public IChair CreateChair()
        {
            return new VictorianChair();
        }

        public ITable CreateTable()
        {
            return new VictorianTable();
        }
    }
}


