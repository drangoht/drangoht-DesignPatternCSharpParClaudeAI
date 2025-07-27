using System;

namespace Patterns.Creational.FactoryMethod
{
    /// <summary>
    /// ConcreteProduct - Document PDF
    /// </summary>
    public class PdfDocument : IDocument
    {
        private string _content;

        public PdfDocument()
        {
            _content = string.Empty;
            Console.WriteLine("Document PDF créé.");
        }

        public void AddContent(string content)
        {
            _content += content;
            Console.WriteLine($"Contenu ajouté au document PDF: {content}");
        }

        public void Save()
        {
            Console.WriteLine($"Document PDF enregistré avec le contenu: {_content}");
        }
    }
}
