using System;

namespace Patterns.Behavioral.Visitor
{
    /// <summary>
    /// Test du pattern Visitor
    /// </summary>
    public class VisitorTest : Patterns.Behavioral.IPatternTest
    {
        public void Run()
        {
            Console.WriteLine("Démonstration du pattern Visitor");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Le pattern Visitor représente une opération à effectuer sur les éléments d'une structure d'objets sans changer les classes sur lesquelles il opère.");
            Console.WriteLine();
            
            // Code de démonstration du pattern
            Console.WriteLine("Exemple du pattern en action:");
            try 
            {
                // Exécuter le code du pattern
                RunPatternDemo();
                Console.WriteLine("✅ Test du pattern Visitor réussi!");
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("La démonstration complète n'est pas encore implémentée.");
                Console.WriteLine("Consultez le code source pour plus de détails sur ce pattern.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erreur lors de l'exécution: {ex.Message}");
                throw; // Rethrow to ensure test failure
            }
        }

        private static void RunPatternDemo()
        {
            // Création de la structure d'objets
            Console.WriteLine("Création d'un document avec divers éléments...");
            Document document = new Document("Mon Document", "Auteur");
            
            // Ajout d'éléments au document
            document.AddElement(new TextElement("Introduction au pattern Visitor", true));
            document.AddElement(new TextElement("Le pattern Visitor permet de séparer les algorithmes des objets sur lesquels ils opèrent.", false, true));
            document.AddElement(new ImageElement("visitor_diagram.png", "Diagramme du pattern Visitor", 640, 480));
            
            // Création d'un tableau
            string[] headers = { "Avantages", "Inconvénients" };
            TableElement table = new TableElement(headers);
            table.AddRow(new string[] { "Séparation des responsabilités", "Violation du principe d'encapsulation" });
            table.AddRow(new string[] { "Facilité d'ajout de nouvelles opérations", "Difficulté d'ajout de nouveaux éléments" });
            document.AddElement(table);
            
            const string url = "https://example.com/patterns/visitor";
            document.AddElement(new HyperlinkElement(url, "Plus d'informations"));
            
            // Affichage du document
            Console.WriteLine("\n1. Affichage du contenu brut du document:");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine(document.GetContent());
            foreach (var element in document.GetElements())
            {
                Console.WriteLine($"- {element.GetContent()}");
            }
            
            // Utilisation des visiteurs
            Console.WriteLine("\n2. Application du visiteur HtmlExportVisitor:");
            Console.WriteLine("-------------------------------------------");
            HtmlExportVisitor htmlVisitor = new HtmlExportVisitor();
            document.Accept(htmlVisitor);
            Console.WriteLine(htmlVisitor.GetResult());
            
            Console.WriteLine("\n3. Application du visiteur StatisticsVisitor:");
            Console.WriteLine("-------------------------------------------");
            StatisticsVisitor statsVisitor = new StatisticsVisitor();
            document.Accept(statsVisitor);
            Console.WriteLine(statsVisitor.GetSummary());
            
            Console.WriteLine("\n4. Application du visiteur MarkdownExportVisitor:");
            Console.WriteLine("------------------------------------------------");
            MarkdownExportVisitor markdownVisitor = new MarkdownExportVisitor();
            document.Accept(markdownVisitor);
            Console.WriteLine(markdownVisitor.GetResult());
            
            Console.WriteLine("\nLe pattern Visitor permet de:");
            Console.WriteLine("- Ajouter de nouvelles opérations aux objets sans les modifier");
            Console.WriteLine("- Regrouper des opérations connexes dans une classe de visiteur");
            Console.WriteLine("- Accumuler des informations sur une structure d'objets");
        }

        public string GetName()
        {
            return "Visitor";
        }

        public string GetDescription()
        {
            return "Le pattern Visitor représente une opération à effectuer sur les éléments d'une structure d'objets sans changer les classes sur lesquelles il opère.";
        }
    }
}

