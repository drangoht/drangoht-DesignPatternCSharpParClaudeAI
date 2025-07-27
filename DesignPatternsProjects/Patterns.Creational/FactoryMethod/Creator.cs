using System;

namespace Patterns.Creational.FactoryMethod
{
    /// <summary>
    /// Creator abstrait - déclare la méthode Factory Method
    /// </summary>
    public abstract class DocumentCreator
    {
        /// <summary>
        /// Factory Method - les sous-classes doivent implémenter cette méthode
        /// </summary>
        public abstract IDocument CreateDocument();

        /// <summary>
        /// Méthode qui utilise le Factory Method
        /// </summary>
        public void OpenDocument()
        {
            // Appelle le Factory Method pour créer un objet Document
            var document = CreateDocument();

            // Utilise l'objet créé
            document.AddContent("content");
            document.Save();
        }
    }

    /// <summary>
    /// Concrete Creator - implémente le Factory Method pour créer des documents PDF
    /// </summary>
    public class PdfCreator : DocumentCreator
    {
        public override IDocument CreateDocument()
        {
            return new PdfDocument();
        }
    }

    /// <summary>
    /// Concrete Creator - implémente le Factory Method pour créer des documents Word
    /// </summary>
    public class WordCreator : DocumentCreator
    {
        public override IDocument CreateDocument()
        {
            return new WordDocument();
        }
    }

}
