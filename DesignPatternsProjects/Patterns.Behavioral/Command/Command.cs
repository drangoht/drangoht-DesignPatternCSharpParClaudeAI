using System;
using System.Collections.Generic;

namespace Patterns.Behavioral.Command
{
    /// <summary>
    /// Interface Command - Déclare une méthode pour exécuter une commande
    /// </summary>
    public interface ICommand
    {
        void Execute();
        void Undo();
        string GetDescription();
    }

    /// <summary>
    /// Receiver - Classe qui sait comment effectuer les opérations associées à une commande
    /// </summary>
    public class TextEditor
    {
        private string _text = "";
        
        public void InsertText(string text)
        {
            Console.WriteLine($"TextEditor: Insertion du texte \"{text}\"");
            _text += text;
        }
        
        public void DeleteText(int length)
        {
            if (length <= _text.Length)
            {
                string deletedText = _text.Substring(_text.Length - length);
                _text = _text.Substring(0, _text.Length - length);
                Console.WriteLine($"TextEditor: Suppression du texte \"{deletedText}\"");
            }
            else
            {
                Console.WriteLine("TextEditor: Impossible de supprimer - texte trop court");
            }
        }
        
        public void FormatText(string format)
        {
            Console.WriteLine($"TextEditor: Application du formatage \"{format}\" au texte");
        }
        
        public string GetText()
        {
            return _text;
        }
    }

    /// <summary>
    /// ConcreteCommand - Définit une liaison entre un Receiver et une action
    /// </summary>
    public class InsertTextCommand : ICommand
    {
        private readonly TextEditor _editor;
        private readonly string _text;
        
        public InsertTextCommand(TextEditor editor, string text)
        {
            _editor = editor;
            _text = text;
        }
        
        public void Execute()
        {
            _editor.InsertText(_text);
        }
        
        public void Undo()
        {
            _editor.DeleteText(_text.Length);
        }
        
        public string GetDescription()
        {
            return $"Insérer \"{_text}\"";
        }
    }

    /// <summary>
    /// ConcreteCommand - Une autre commande concrète
    /// </summary>
    public class DeleteTextCommand : ICommand
    {
        private readonly TextEditor _editor;
        private readonly int _length;
        private string _deletedText = string.Empty;
        
        public DeleteTextCommand(TextEditor editor, int length)
        {
            _editor = editor;
            _length = length;
        }
        
        public void Execute()
        {
            _deletedText = _editor.GetText();
            if (_length <= _deletedText.Length)
            {
                _deletedText = _deletedText.Substring(_deletedText.Length - _length);
                _editor.DeleteText(_length);
            }
            else
            {
                _deletedText = "";
                Console.WriteLine("Command: Impossible d'exécuter la suppression - texte trop court");
            }
        }
        
        public void Undo()
        {
            if (!string.IsNullOrEmpty(_deletedText))
            {
                _editor.InsertText(_deletedText);
            }
        }
        
        public string GetDescription()
        {
            return $"Supprimer {_length} caractères";
        }
    }

    /// <summary>
    /// ConcreteCommand - Une troisième commande concrète
    /// </summary>
    public class FormatTextCommand : ICommand
    {
        private readonly TextEditor _editor;
        private readonly string _format;
        
        public FormatTextCommand(TextEditor editor, string format)
        {
            _editor = editor;
            _format = format;
        }
        
        public void Execute()
        {
            _editor.FormatText(_format);
        }
        
        public void Undo()
        {
            Console.WriteLine($"FormatTextCommand: Annulation du formatage \"{_format}\"");
        }
        
        public string GetDescription()
        {
            return $"Formater en \"{_format}\"";
        }
    }
    
    /// <summary>
    /// ConcreteCommand - Commande pour mettre le texte en majuscules
    /// </summary>
    public class UpperCaseCommand : ICommand
    {
        private readonly TextEditor _editor;
        private string _originalText;
        
        public UpperCaseCommand(TextEditor editor)
        {
            _editor = editor;
            _originalText = string.Empty;
        }
        
        public void Execute()
        {
            // Sauvegarder l'état original pour pouvoir annuler
            _originalText = _editor.GetText();
            
            // Supprimer le texte actuel
            _editor.DeleteText(_originalText.Length);
            
            // Insérer le texte en majuscules
            _editor.InsertText(_originalText.ToUpper());
            
            Console.WriteLine("UpperCaseCommand: Texte converti en majuscules");
        }
        
        public void Undo()
        {
            // Supprimer le texte en majuscules
            _editor.DeleteText(_editor.GetText().Length);
            
            // Restaurer le texte original
            _editor.InsertText(_originalText);
            
            Console.WriteLine("UpperCaseCommand: Restauration du texte original");
        }
        
        public string GetDescription()
        {
            return "Convertir en majuscules";
        }
    }

    /// <summary>
    /// Invoker - Demande au Command d'exécuter la requête
    /// </summary>
    public class CommandInvoker
    {
        private readonly Stack<ICommand> _undoStack = new Stack<ICommand>();
        private readonly Stack<ICommand> _redoStack = new Stack<ICommand>();
        
        public void ExecuteCommand(ICommand command)
        {
            Console.WriteLine($"Invoker: Exécution de la commande \"{command.GetDescription()}\"");
            command.Execute();
            _undoStack.Push(command);
            _redoStack.Clear(); // Une nouvelle commande invalide l'historique des Redo
        }
        
        public void Undo()
        {
            if (_undoStack.Count > 0)
            {
                ICommand command = _undoStack.Pop();
                Console.WriteLine($"Invoker: Annulation de la commande \"{command.GetDescription()}\"");
                command.Undo();
                _redoStack.Push(command);
            }
            else
            {
                Console.WriteLine("Invoker: Rien à annuler");
            }
        }
        
        public void Redo()
        {
            if (_redoStack.Count > 0)
            {
                ICommand command = _redoStack.Pop();
                Console.WriteLine($"Invoker: Rétablissement de la commande \"{command.GetDescription()}\"");
                command.Execute();
                _undoStack.Push(command);
            }
            else
            {
                Console.WriteLine("Invoker: Rien à rétablir");
            }
        }
    }
}


