using System;

namespace Patterns.Creational.FactoryMethod
{
    /// <summary>
    /// ConcreteProduct - Document Word
    /// </summary>
    public class WordDocument : IDocument
    {
        private string _content;

        public WordDocument()
        {
            _content = string.Empty;
            Console.WriteLine("Document Word créé.");
        }

        public void AddContent(string content)
        {
            _content += content;
            Console.WriteLine($"Contenu ajouté au document Word: {content}");
        }

        public void Save()
        {
            Console.WriteLine($"Document Word enregistré avec le contenu: {_content}");
        }
    }
}
