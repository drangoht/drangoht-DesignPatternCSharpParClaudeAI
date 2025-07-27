using System;
using Xunit;

namespace Patterns.Behavioral.Command.Tests
{
    public class CommandTest
    {
        [Fact]
        public void TestCommandPattern()
        {
            // Créer l'éditeur et le gestionnaire de commandes
            var editor = new TextEditor();
            var commandManager = new CommandManager();

            // Test 1: Insertion de texte
            Console.WriteLine("=== Test d'insertion de texte ===");
            var insertCommand = new InsertTextCommand(editor, "Hello");
            commandManager.ExecuteCommand(insertCommand);
            Assert.Equal("Hello", editor.GetText());
            Assert.Equal(5, editor.GetCursorPosition());

            // Test 2: Déplacement du curseur
            Console.WriteLine("\n=== Test de déplacement du curseur ===");
            var moveCommand = new MoveCursorCommand(editor, -2);
            commandManager.ExecuteCommand(moveCommand);
            Assert.Equal(3, editor.GetCursorPosition());

            // Test 3: Insertion au milieu
            Console.WriteLine("\n=== Test d'insertion au milieu ===");
            var insertMiddleCommand = new InsertTextCommand(editor, "p!");
            commandManager.ExecuteCommand(insertMiddleCommand);
            Assert.Equal("Help!lo", editor.GetText());

            // Test 4: Suppression
            Console.WriteLine("\n=== Test de suppression ===");
            var deleteCommand = new DeleteTextCommand(editor, 2);
            commandManager.ExecuteCommand(deleteCommand);
            Assert.Equal("Hello", editor.GetText());

            // Afficher l'historique
            commandManager.ShowHistory();

            // Test 5: Undo/Redo
            Console.WriteLine("\n=== Test d'annulation et de rétablissement ===");
            
            // Annuler la dernière suppression
            commandManager.Undo();
            Assert.Equal("Help!lo", editor.GetText());

            // Annuler l'insertion du milieu
            commandManager.Undo();
            Assert.Equal("Hello", editor.GetText());

            // Annuler le déplacement du curseur
            commandManager.Undo();
            Assert.Equal("Hello", editor.GetText());
            Assert.Equal(5, editor.GetCursorPosition());

            // Annuler l'insertion initiale
            commandManager.Undo();
            Assert.Equal("", editor.GetText());

            // Rétablir toutes les commandes
            commandManager.Redo(); // Insertion initiale
            commandManager.Redo(); // Déplacement du curseur
            commandManager.Redo(); // Insertion au milieu
            commandManager.Redo(); // Suppression
            Assert.Equal("Hello", editor.GetText());

            // Test 6: Nouvelle commande après undo
            Console.WriteLine("\n=== Test de nouvelle commande après undo ===");
            commandManager.Undo(); // Retour à "Help!lo"
            var newCommand = new InsertTextCommand(editor, " World");
            commandManager.ExecuteCommand(newCommand);
            Assert.Equal("Help!lo World", editor.GetText());

            // Le redo ne devrait plus être possible
            commandManager.Redo();
            Assert.Equal("Help!lo World", editor.GetText());

            // Afficher l'historique final
            commandManager.ShowHistory();
        }
    }
}
