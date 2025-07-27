using System;

namespace Patterns.Behavioral.Memento
{
    /// <summary>
    /// Test du pattern Memento
    /// </summary>
    public class MementoTest : Patterns.Behavioral.IPatternTest
    {
        public void Run()
        {
            Console.WriteLine("Démonstration du pattern Memento");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Le pattern Memento capture et externalise l'état interne d'un objet sans violer l'encapsulation.");
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

        private static void RunPatternDemo()
        {
            // Création de l'éditeur (Originator)
            TextEditor editor = new TextEditor();
            
            // Création du gestionnaire d'historique (Caretaker)
            EditorHistory history = new EditorHistory();

            // État initial - sauvegarde
            history.SaveState(editor);
            
            // Première modification
            editor.Type("Bonjour, ceci est un test du pattern Memento.");
            editor.DisplayState();
            history.SaveState(editor);

            // Deuxième modification - sélection et suppression
            editor.SelectText(8, 4);
            editor.DisplayState();
            history.SaveState(editor);

            editor.DeleteSelection();
            editor.DisplayState();
            history.SaveState(editor);

            // Troisième modification - ajout de texte et formatage
            editor.Type(" IMPORTANT ");
            editor.ApplyFormatting(true, false, true);
            editor.DisplayState();
            history.SaveState(editor);

            // Quatrième modification
            editor.SelectText(0, 7);
            editor.Type("Salut");
            editor.DisplayState();
            history.SaveState(editor);

            // Afficher l'historique des états
            history.ShowHistory();

            // Test des opérations Undo/Redo
            Console.WriteLine("\n--- Test des opérations Undo/Redo ---");
            
            // Annuler (revenir à l'état précédent)
            history.Undo(editor);
            editor.DisplayState();
            
            // Annuler encore
            history.Undo(editor);
            editor.DisplayState();
            
            // Rétablir
            history.Redo(editor);
            editor.DisplayState();
            
            // Une autre annulation
            history.Undo(editor);
            editor.DisplayState();
            
            // Nouvel état qui efface la pile "redo"
            editor.Type(" Ceci est une nouvelle modification.");
            editor.DisplayState();
            history.SaveState(editor);
            
            // Tentative de rétablissement (devrait échouer car nous avons fait une nouvelle modification)
            history.Redo(editor);
            
            // Afficher l'historique final
            history.ShowHistory();
        }

        public string GetName()
        {
            return "Memento";
        }

        public string GetDescription()
        {
            return "Le pattern Memento capture et externalise l'état interne d'un objet sans violer l'encapsulation.";
        }
    }
}

