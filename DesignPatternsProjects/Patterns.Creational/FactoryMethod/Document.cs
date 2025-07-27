using System;

namespace Patterns.Creational.FactoryMethod
{
    /// <summary>
    /// Product - Interface Document
    /// </summary>
    public interface IDocument
    {
        void AddContent(string content);
        void Save();
    }
}
