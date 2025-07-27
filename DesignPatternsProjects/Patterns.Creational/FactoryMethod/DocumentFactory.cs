using System;

namespace Patterns.Creational.FactoryMethod
{
    /// <summary>
    /// Creator - Interface Fabrique de documents
    /// </summary>
    public interface IDocumentFactory
    {
        IDocument CreateDocument();
    }

    /// <summary>
    /// ConcreteCreator - Fabrique de documents Word
    /// </summary>
    public class WordDocumentFactory : IDocumentFactory
    {
        public IDocument CreateDocument()
        {
            var doc = new WordDocument();
            doc.AddContent("wordContent");
            return doc;
        }
    }

    /// <summary>
    /// ConcreteCreator - Fabrique de documents PDF
    /// </summary>
    public class PdfDocumentFactory : IDocumentFactory
    {
        public IDocument CreateDocument()
        {
            var doc = new PdfDocument();
            doc.AddContent("pdfContent");
            return doc;
        }
    }

}
