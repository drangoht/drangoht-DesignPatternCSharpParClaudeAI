using System;
using Xunit;

namespace Patterns.Behavioral.Visitor.Tests
{
    public class VisitorTest
    {
        [Fact]
        public void TestVisitorPattern()
        {
            Console.WriteLine("=== Test du pattern Visitor avec un document ===\n");

            // Créer un document avec différents éléments
            var document = new Document();
            
            document.Add(new Paragraph("Bienvenue sur notre site!", "bold"));
            document.Add(new Paragraph("Voici une présentation de nos services."));
            
            document.Add(new Image("logo.png", "Logo de l'entreprise", 100, 50));
            
            document.Add(new Hyperlink("Contactez-nous", "https://example.com/contact"));
            
            var table = new Table(2, 3);
            table.SetCell(0, 0, "Produit");
            table.SetCell(0, 1, "Prix");
            table.SetCell(0, 2, "Stock");
            table.SetCell(1, 0, "Widget");
            table.SetCell(1, 1, "9.99");
            table.SetCell(1, 2, "42");
            document.Add(table);

            // Test 1: Conversion en HTML
            Console.WriteLine("1. Test de la conversion en HTML:");
            var htmlVisitor = new HtmlVisitor();
            document.Accept(htmlVisitor);
            var html = htmlVisitor.GetOutput();
            Console.WriteLine(html);

            Assert.Contains("<p class=\"bold\">", html);
            Assert.Contains("<img src=\"logo.png\"", html);
            Assert.Contains("<a href=\"https://example.com/contact\"", html);
            Assert.Contains("<table>", html);

            // Test 2: Conversion en Markdown
            Console.WriteLine("\n2. Test de la conversion en Markdown:");
            var markdownVisitor = new MarkdownVisitor();
            document.Accept(markdownVisitor);
            var markdown = markdownVisitor.GetOutput();
            Console.WriteLine(markdown);

            Assert.Contains("**Bienvenue sur notre site!**", markdown);
            Assert.Contains("![Logo de l'entreprise](logo.png)", markdown);
            Assert.Contains("[Contactez-nous](https://example.com/contact)", markdown);
            Assert.Contains("| Header | Header | Header |", markdown);

            // Test 3: Statistiques du document
            Console.WriteLine("\n3. Test des statistiques:");
            var statsVisitor = new StatisticsVisitor();
            document.Accept(statsVisitor);
            Console.WriteLine(statsVisitor);

            Assert.Equal(2, statsVisitor.ParagraphCount);
            Assert.Equal(1, statsVisitor.ImageCount);
            Assert.Equal(1, statsVisitor.LinkCount);
            Assert.Equal(6, statsVisitor.TableCellCount);
            
            // Test des comptages de mots
            Assert.Equal(8, statsVisitor.WordCount); // "Bienvenue sur notre site" + "Contactez-nous"

            // Test 4: Document vide
            Console.WriteLine("\n4. Test avec un document vide:");
            var emptyDocument = new Document();
            
            var emptyStats = new StatisticsVisitor();
            emptyDocument.Accept(emptyStats);
            
            Assert.Equal(0, emptyStats.ParagraphCount);
            Assert.Equal(0, emptyStats.WordCount);
            Assert.Equal(0, emptyStats.LinkCount);
            Assert.Equal(0, emptyStats.ImageCount);
            Assert.Equal(0, emptyStats.TableCellCount);

            // Test 5: Document complexe
            Console.WriteLine("\n5. Test avec un document complexe:");
            var complexDocument = new Document();
            
            // Ajouter beaucoup d'éléments variés
            complexDocument.Add(new Paragraph("Titre principal", "bold"));
            complexDocument.Add(new Image("banner.jpg", "Bannière", 800, 200));
            complexDocument.Add(new Paragraph("Premier paragraphe"));
            complexDocument.Add(new Hyperlink("Lien 1", "https://example.com/1"));
            complexDocument.Add(new Paragraph("Deuxième paragraphe"));
            complexDocument.Add(new Hyperlink("Lien 2", "https://example.com/2"));
            
            var bigTable = new Table(3, 4);
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 4; j++)
                    bigTable.SetCell(i, j, $"Cell {i},{j}");
            complexDocument.Add(bigTable);

            // Vérifier les statistiques
            var complexStats = new StatisticsVisitor();
            complexDocument.Accept(complexStats);
            
            Assert.Equal(3, complexStats.ParagraphCount);
            Assert.Equal(1, complexStats.ImageCount);
            Assert.Equal(2, complexStats.LinkCount);
            Assert.Equal(12, complexStats.TableCellCount);

            // Vérifier que les deux formats de sortie fonctionnent
            var complexHtml = new HtmlVisitor();
            var complexMarkdown = new MarkdownVisitor();
            
            complexDocument.Accept(complexHtml);
            complexDocument.Accept(complexMarkdown);
            
            Assert.NotEmpty(complexHtml.GetOutput());
            Assert.NotEmpty(complexMarkdown.GetOutput());
        }
    }
}
