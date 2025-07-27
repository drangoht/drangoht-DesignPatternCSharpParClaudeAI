using System;

namespace Patterns.Behavioral.TemplateMethod
{
    /// <summary>
    /// Test du pattern TemplateMethod
    /// </summary>
    public class TemplateMethodTest : Patterns.Behavioral.IPatternTest
    {
        public void Run()
        {
            Console.WriteLine("Démonstration du pattern TemplateMethod");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Le pattern Template Method définit le squelette d'un algorithme dans une méthode, en déléguant certaines étapes aux sous-classes.");
            Console.WriteLine();
            
            // Code de démonstration du pattern
            Console.WriteLine("Exemple du pattern en action:");
            try 
            {
                // Exécuter le code du pattern
                RunPatternDemo();
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("La démonstration complète n'est pas encore implémentée.");
                Console.WriteLine("Consultez le code source pour plus de détails sur ce pattern.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'exécution: {ex.Message}");
            }
        }

        private void RunPatternDemo()
        {
            Console.WriteLine("1. Génération d'un rapport d'entreprise:");
            DocumentGenerator reportGenerator = new ReportGenerator();
            reportGenerator.GenerateDocument();
            
            Console.WriteLine("2. Génération d'un e-mail:");
            DocumentGenerator emailGenerator = new EmailGenerator();
            emailGenerator.GenerateDocument();
            
            Console.WriteLine("3. Génération d'un article de recherche:");
            DocumentGenerator paperGenerator = new ResearchPaperGenerator();
            paperGenerator.GenerateDocument();
            
            Console.WriteLine("Le pattern Template Method a permis de:");
            Console.WriteLine("- Définir un squelette d'algorithme dans la classe abstraite DocumentGenerator");
            Console.WriteLine("- Laisser les sous-classes redéfinir certaines étapes sans changer la structure de l'algorithme");
            Console.WriteLine("- Réutiliser le code commun tout en personnalisant certaines parties");
            Console.WriteLine("- Utiliser des 'hooks' pour des comportements optionnels (tableOfContents, appendix)");
        }

        public string GetName()
        {
            return "TemplateMethod";
        }

        public string GetDescription()
        {
            return "Le pattern Template Method définit le squelette d'un algorithme dans une méthode, en déléguant certaines étapes aux sous-classes.";
        }
    }
}

