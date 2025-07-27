using System;
using Xunit;

namespace Patterns.Structural.Composite.Tests
{
    public class CompositeTest
    {
        [Fact]
        public void TestCompositePattern()
        {
            // Créer une structure de répertoires et fichiers
            var root = new Directory("root");
            
            var docs = new Directory("documents");
            var pictures = new Directory("pictures");
            
            var file1 = new File("document1.txt", 100);
            var file2 = new File("document2.txt", 200);
            var picture1 = new File("picture1.jpg", 500);
            var picture2 = new File("picture2.jpg", 700);

            // Construire la hiérarchie
            root.Add(docs);
            root.Add(pictures);
            
            docs.Add(file1);
            docs.Add(file2);
            
            pictures.Add(picture1);
            pictures.Add(picture2);

            // Tester la taille totale
            Assert.Equal(1500, root.GetSize());
            Assert.Equal(300, docs.GetSize());
            Assert.Equal(1200, pictures.GetSize());

            // Tester les chemins
            Assert.Equal(@"root\documents\document1.txt", file1.GetPath());
            Assert.Equal(@"root\pictures\picture1.jpg", picture1.GetPath());
            Assert.Equal(@"root\documents", docs.GetPath());

            // Tester la structure
            var docsChildren = docs.GetChildren();
            Assert.Equal(2, docsChildren.Count);
            Assert.Contains(file1, docsChildren);
            Assert.Contains(file2, docsChildren);

            // Tester les exceptions
            var testFile = new File("test.txt", 100);
            Assert.Throws<InvalidOperationException>(() => testFile.Add(new File("invalid.txt", 50)));
            Assert.Throws<InvalidOperationException>(() => testFile.Remove(new File("invalid.txt", 50)));

            // Test d'affichage (vérifie juste qu'il n'y a pas d'exception)
            root.Display();

            // Test de suppression
            docs.Remove(file1);
            Assert.Equal(200, docs.GetSize());
            Assert.Single(docs.GetChildren());
        }
    }
}
