using System;

namespace Patterns.Creational.AbstractFactory
{
    /// <summary>
    /// Interface abstraite pour un produit de type chaise
    /// </summary>
    public interface IChair
    {
        void SitOn();
        string GetStyle();
    }

    /// <summary>
    /// Interface abstraite pour un produit de type table
    /// </summary>
    public interface ITable
    {
        void PutOn(string item);
        string GetStyle();
    }

    /// <summary>
    /// Chaise moderne concrète
    /// </summary>
    public class ModernChair : IChair
    {
        public void SitOn()
        {
            Console.WriteLine("Vous êtes assis sur une chaise au design moderne.");
        }

        public string GetStyle()
        {
            return "Moderne";
        }
    }

    /// <summary>
    /// Table moderne concrète
    /// </summary>
    public class ModernTable : ITable
    {
        public void PutOn(string item)
        {
            Console.WriteLine($"Vous avez posé {item} sur une table au design moderne.");
        }

        public string GetStyle()
        {
            return "Moderne";
        }
    }

    /// <summary>
    /// Chaise victorienne concrète
    /// </summary>
    public class VictorianChair : IChair
    {
        public void SitOn()
        {
            Console.WriteLine("Vous êtes assis sur une chaise au style victorien ornementé.");
        }

        public string GetStyle()
        {
            return "Victorien";
        }
    }

    /// <summary>
    /// Table victorienne concrète
    /// </summary>
    public class VictorianTable : ITable
    {
        public void PutOn(string item)
        {
            Console.WriteLine($"Vous avez posé {item} sur une table au style victorien ornementé.");
        }

        public string GetStyle()
        {
            return "Victorien";
        }
    }
}


