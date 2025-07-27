using System;

namespace Patterns.Behavioral.Command
{
    /// <summary>
    /// Classe de test pour démontrer le pattern Command
    /// </summary>
    public class CommandTest : Patterns.Behavioral.IPatternTest
    {
        public void Run()
        {
            Console.WriteLine("Test du pattern Command");
            Console.WriteLine("----------------------");
            Console.WriteLine("Le pattern Command transforme une requête en objet, facilitant");
            Console.WriteLine("la paramétrisation des clients avec différentes requêtes,");
            Console.WriteLine("la mise en file d'attente des requêtes, et la prise en charge");
            Console.WriteLine("des opérations réversibles (undo/redo).\n");

            // Création du receiver (l'objet qui exécute les opérations)
            TextEditor editor = new TextEditor();
            
            // Création de l'invoker (objet qui déclenche les commandes)
            CommandInvoker invoker = new CommandInvoker();
            
            // Création et exécution de commandes
            Console.WriteLine("Séquence d'édition de texte:");
            Console.WriteLine("---------------------------");
            
            // Commande 1: Insérer du texte
            ICommand insertHello = new InsertTextCommand(editor, "Hello, ");
            invoker.ExecuteCommand(insertHello);
            
            // Commande 2: Insérer du texte
            ICommand insertWorld = new InsertTextCommand(editor, "World!");
            invoker.ExecuteCommand(insertWorld);
            
            // Affichage du texte actuel
            Console.WriteLine($"\nTexte actuel: {editor.GetText()}\n");
            
            // Commande 3: Modifier la casse
            ICommand toUpper = new UpperCaseCommand(editor);
            invoker.ExecuteCommand(toUpper);
            
            // Affichage du texte après modification
            Console.WriteLine($"Texte après modification: {editor.GetText()}\n");
            
            // Démonstration d'undo
            Console.WriteLine("Annulation de la dernière commande (UpperCase):");
            invoker.Undo();
            Console.WriteLine($"Texte après annulation: {editor.GetText()}\n");
            
            // Démonstration d'undo multiple
            Console.WriteLine("Annulation des commandes précédentes:");
            invoker.Undo(); // Annule insertWorld
            Console.WriteLine($"Après annulation 2: {editor.GetText()}");
            invoker.Undo(); // Annule insertHello
            Console.WriteLine($"Après annulation 3: {editor.GetText()}\n");
            
            // Démonstration de redo
            Console.WriteLine("Rétablissement des commandes annulées:");
            invoker.Redo(); // Refait insertHello
            Console.WriteLine($"Après rétablissement 1: {editor.GetText()}");
            invoker.Redo(); // Refait insertWorld
            Console.WriteLine($"Après rétablissement 2: {editor.GetText()}");
            invoker.Redo(); // Refait toUpper
            Console.WriteLine($"Après rétablissement 3: {editor.GetText()}\n");
            
            Console.WriteLine("Avantages du pattern Command:");
            Console.WriteLine("1. Il encapsule une requête sous forme d'objet, permettant");
            Console.WriteLine("   de mettre les requêtes en file d'attente et de les journaliser.");
            Console.WriteLine("2. Il permet également l'annulation et le rétablissement des opérations");
            Console.WriteLine("   en gardant un historique des commandes exécutées.");
            Console.WriteLine("3. Ce pattern est utile pour implémenter des fonctionnalités comme:");
            Console.WriteLine("   - L'historique des transactions");
            Console.WriteLine("   - Les files d'attente de tâches");
            Console.WriteLine("   - Les opérations réversibles (undo/redo)");
        }
        
        public string GetName()
        {
            return "Command";
        }

        public string GetDescription()
        {
            return "Le pattern Command transforme une requête en objet, facilitant la paramétrisation des clients avec différentes requêtes, la mise en file d'attente des requêtes, et la prise en charge des opérations réversibles.";
        }
    }
}

