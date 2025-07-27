using System;
using System.Collections.Generic;
using Patterns.Common;
using Patterns.Creational;
using Patterns.Structural;
using Patterns.Behavioral;

namespace DesignPatternsMenu
{
    internal static class Program
    {
        private static readonly CreationalPatternManager _creationalManager = new();
        private static readonly StructuralPatternManager _structuralManager = new();
        private static readonly BehavioralPatternManager _behavioralManager = new();

        private static void Main(string[] args)
        {

            // Boucle principale du menu
            bool exit = false;
            while (!exit)
            {
                exit = DisplayMainMenu();
            }
        }

        /// <summary>
        /// Affiche le menu principal et retourne true si l'utilisateur veut quitter
        /// </summary>
        static bool DisplayMainMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔═══════════════════════════════════════════╗");
            Console.WriteLine("║                                           ║");
            Console.WriteLine("║   Design Patterns - Explorateur de GOF    ║");
            Console.WriteLine("║                                           ║");
            Console.WriteLine("╚═══════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Choisissez une catégorie de patterns:");
            Console.WriteLine();
            Console.WriteLine("1. Patterns Créationnels (Creational)");
            Console.WriteLine("2. Patterns Structuraux (Structural)");
            Console.WriteLine("3. Patterns Comportementaux (Behavioral)");
            Console.WriteLine();
            Console.WriteLine("0. Quitter");
            Console.WriteLine();
            Console.Write("Votre choix: ");

            string? input = Console.ReadLine();
            
            switch (input ?? string.Empty)
            {
                case "1":
                    DisplayPatternCategoryMenu("Créationnels", _creationalManager.GetPatterns(), pattern => _creationalManager.RunPattern(pattern));
                    return false;
                case "2":
                    DisplayPatternCategoryMenu("Structuraux", _structuralManager.GetPatterns(), pattern => _structuralManager.RunPattern(pattern));
                    return false;
                case "3":
                    DisplayPatternCategoryMenu("Comportementaux", _behavioralManager.GetPatterns(), pattern => _behavioralManager.RunPattern(pattern));
                    return false;
                case "0":
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Affiche le menu d'une catégorie de patterns
        /// </summary>
        static void DisplayPatternCategoryMenu<T>(string categoryName, IReadOnlyList<T> patterns, Action<int> runPattern) where T : Patterns.Common.IPatternTest
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"╔═══════════════════════════════════════════╗");
                Console.WriteLine($"║                                           ║");
                Console.WriteLine($"║     Patterns {categoryName,-16}          ║");
                Console.WriteLine($"║                                           ║");
                Console.WriteLine($"╚═══════════════════════════════════════════╝");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("Choisissez un pattern à exécuter:");
                Console.WriteLine();

                for (int i = 0; i < patterns.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {patterns[i].GetName()}");
                }

                Console.WriteLine();
                Console.WriteLine("0. Retour au menu principal");
                Console.WriteLine();
                Console.Write("Votre choix: ");

                string? input = Console.ReadLine() ?? string.Empty;

                if (input == "0")
                {
                    back = true;
                }
                else if (int.TryParse(input, out int patternIndex) && patternIndex > 0 && patternIndex <= patterns.Count)
                {
                    runPattern(patternIndex - 1);
                }
            }
        }
    }
}

