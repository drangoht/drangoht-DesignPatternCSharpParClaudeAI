namespace Patterns.Common
{
    /// <summary>
    /// Interface commune pour tous les tests de patterns
    /// </summary>
    public interface IPatternTest
    {
        /// <summary>
        /// Exécute le test du pattern
        /// </summary>
        void Run();
        
        /// <summary>
        /// Retourne le nom du pattern
        /// </summary>
        string GetName();
        
        /// <summary>
        /// Retourne la description du pattern
        /// </summary>
        string GetDescription();
    }
}
