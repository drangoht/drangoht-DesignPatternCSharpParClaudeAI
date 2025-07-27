using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Patterns.Behavioral
{
    /// <summary>
    /// Gestionnaire des patterns comportementaux
    /// </summary>
    public class BehavioralPatternManager
    {
        private readonly List<IPatternTest> _patterns = new List<IPatternTest>();

        /// <summary>
        /// Constructeur qui découvre et instancie tous les patterns comportementaux disponibles
        /// </summary>
        public BehavioralPatternManager()
        {
            DiscoverPatterns();
        }

        /// <summary>
        /// Découvre et instancie tous les patterns comportementaux disponibles
        /// </summary>
        private void DiscoverPatterns()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var patternTestTypes = assembly.GetTypes()
                .Where(t => typeof(IPatternTest).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .ToList();

            foreach (var type in patternTestTypes)
            {
                try
                {
                    if (Activator.CreateInstance(type) is IPatternTest instance)
                    {
                        _patterns.Add(instance);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de la création du pattern {type.Name}: {ex.Message}");
                }
            }

            // Trier les patterns par nom (sans réassigner la variable readonly)
            _patterns.Sort((a, b) => string.Compare(a.GetName(), b.GetName(), StringComparison.Ordinal));
        }

        /// <summary>
        /// Retourne la liste de tous les patterns comportementaux disponibles
        /// </summary>
        public IReadOnlyList<IPatternTest> GetPatterns()
        {
            return _patterns.AsReadOnly();
        }

        /// <summary>
        /// Exécute un pattern comportemental spécifique
        /// </summary>
        /// <param name="index">L'index du pattern à exécuter</param>
        public void RunPattern(int index)
        {
            if (index >= 0 && index < _patterns.Count)
            {
                var pattern = _patterns[index];
                Console.Clear();
                Console.WriteLine($"=== Pattern {pattern.GetName()} ===");
                Console.WriteLine();
                Console.WriteLine($"Description: {pattern.GetDescription()}");
                Console.WriteLine();
                Console.WriteLine("=== Démonstration ===");
                Console.WriteLine();
                
                pattern.Run();
                
                Console.WriteLine();
                Console.WriteLine("=== Fin de la démonstration ===");
                Console.WriteLine();
                Console.WriteLine("Appuyez sur une touche pour continuer...");
                Console.ReadKey();
            }
        }
    }
}

