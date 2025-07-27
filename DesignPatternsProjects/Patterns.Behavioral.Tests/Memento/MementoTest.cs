using System;
using Xunit;

namespace Patterns.Behavioral.Memento.Tests
{
    public class MementoTest
    {
        [Fact]
        public void TestMementoPattern()
        {
            // Créer l'éditeur et son historique
            var editor = new TextEditor();
            var history = new History(editor);

            Console.WriteLine("=== Test du pattern Memento avec un éditeur de texte ===\n");

            // Test 1: Saisie de texte et formatage
            Console.WriteLine("1. Saisie initiale et formatage");
            editor.Type("Hello ");
            history.Backup();

            editor.ToggleBold();
            editor.Type("World");
            history.Backup();

            editor.SetFont("Times New Roman", 14);
            editor.ToggleItalic();
            history.Backup();

            // Vérifier l'état actuel
            Assert.Equal("Hello World", editor.GetContent());
            Assert.True(editor.IsBold());
            Assert.True(editor.IsItalic());
            Assert.Equal("Times New Roman", editor.GetFontName());
            Assert.Equal(14, editor.GetFontSize());

            // Test 2: Sélection et suppression
            Console.WriteLine("\n2. Test de sélection et suppression");
            editor.Select(6, 5);  // Sélectionne "World"
            Assert.Equal("World", editor.GetSelectedText());
            
            editor.Delete();
            history.Backup();
            
            Assert.Equal("Hello ", editor.GetContent());
            Assert.Equal("", editor.GetSelectedText());

            // Test 3: Annulation (Undo)
            Console.WriteLine("\n3. Test d'annulation");
            history.ShowHistory();
            
            history.Undo();  // Retour avant la suppression
            Assert.Equal("Hello World", editor.GetContent());
            
            history.Undo();  // Retour avant le changement de police
            Assert.Equal("Arial", editor.GetFontName());
            Assert.Equal(12, editor.GetFontSize());
            Assert.False(editor.IsItalic());
            
            history.Undo();  // Retour à la saisie initiale
            Assert.Equal("Hello ", editor.GetContent());
            Assert.False(editor.IsBold());

            // Test 4: Rétablissement (Redo)
            Console.WriteLine("\n4. Test de rétablissement");
            history.Redo();  // Retour au texte en gras
            Assert.True(editor.IsBold());
            Assert.Equal("Hello World", editor.GetContent());

            history.Redo();  // Retour au changement de police
            Assert.Equal("Times New Roman", editor.GetFontName());
            Assert.Equal(14, editor.GetFontSize());
            Assert.True(editor.IsItalic());

            // Test 5: Nouvelle action après Undo
            Console.WriteLine("\n5. Test de nouvelle action après Undo");
            history.Undo();  // Retour en arrière
            history.Undo();  // Encore en arrière
            
            editor.Type("Universe");  // Nouvelle action
            history.Backup();
            
            // Le Redo ne devrait plus être possible
            history.Redo();
            Assert.Equal("Hello Universe", editor.GetContent());

            // Test 6: Limites de l'historique
            Console.WriteLine("\n6. Test des limites de l'historique");
            while (editor.GetContent().Length > 0)
            {
                editor.Delete();
            }
            history.Backup();
            
            // Tenter d'annuler plus que possible
            for (int i = 0; i < 10; i++)
            {
                history.Undo();
            }
            
            // Tenter de rétablir plus que possible
            for (int i = 0; i < 10; i++)
            {
                history.Redo();
            }

            // Afficher l'historique final
            history.ShowHistory();
        }
    }
}
