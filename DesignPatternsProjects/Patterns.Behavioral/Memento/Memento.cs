using System;
using System.Collections.Generic;

namespace Patterns.Behavioral.Memento
{
    /// <summary>
    /// Originator - Classe dont l'état doit être sauvegardé et restauré
    /// </summary>
    public class TextEditor
    {
        private string _content;
        private int _cursorPosition;
        private string _selectedText;
        private TextFormatting _formatting;

        public TextEditor()
        {
            _content = string.Empty;
            _cursorPosition = 0;
            _selectedText = string.Empty;
            _formatting = new TextFormatting();
        }

        // Propriétés pour accéder à l'état de l'éditeur
        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                _cursorPosition = Math.Min(_cursorPosition, _content.Length);
            }
        }

        public int CursorPosition
        {
            get { return _cursorPosition; }
            set { _cursorPosition = Math.Min(Math.Max(0, value), _content.Length); }
        }

        public string SelectedText
        {
            get { return _selectedText; }
            set { _selectedText = value; }
        }

        public TextFormatting Formatting
        {
            get { return _formatting; }
            set { _formatting = value; }
        }

        // Méthodes pour modifier l'état de l'éditeur
        public void Type(string text)
        {
            // Supprimer le texte sélectionné s'il y en a un
            if (!string.IsNullOrEmpty(_selectedText))
            {
                int selectionStart = _content.IndexOf(_selectedText);
                if (selectionStart >= 0)
                {
                    _content = _content.Remove(selectionStart, _selectedText.Length);
                    _cursorPosition = selectionStart;
                }
                _selectedText = string.Empty;
            }

            // Insérer le nouveau texte à la position du curseur
            _content = _content.Insert(_cursorPosition, text);
            _cursorPosition += text.Length;
        }

        public void DeleteSelection()
        {
            if (!string.IsNullOrEmpty(_selectedText))
            {
                int selectionStart = _content.IndexOf(_selectedText);
                if (selectionStart >= 0)
                {
                    _content = _content.Remove(selectionStart, _selectedText.Length);
                    _cursorPosition = selectionStart;
                    _selectedText = string.Empty;
                }
            }
        }

        public void SelectText(int startPosition, int length)
        {
            if (startPosition >= 0 && startPosition < _content.Length)
            {
                int actualLength = Math.Min(length, _content.Length - startPosition);
                _selectedText = _content.Substring(startPosition, actualLength);
                _cursorPosition = startPosition + actualLength;
            }
        }

        public void ApplyFormatting(bool isBold, bool isItalic, bool isUnderlined)
        {
            _formatting = new TextFormatting
            {
                IsBold = isBold,
                IsItalic = isItalic,
                IsUnderlined = isUnderlined
            };
        }

        // Affiche l'état actuel de l'éditeur
        public void DisplayState()
        {
            Console.WriteLine("--- État de l'éditeur ---");
            Console.WriteLine($"Contenu: \"{_content}\"");
            Console.WriteLine($"Position du curseur: {_cursorPosition}");
            Console.WriteLine($"Texte sélectionné: \"{_selectedText}\"");
            Console.WriteLine($"Formatage: {_formatting}");
            Console.WriteLine("------------------------");
        }

        // Crée un memento contenant l'état actuel
        public EditorMemento CreateMemento()
        {
            return new EditorMemento(
                _content,
                _cursorPosition,
                _selectedText,
                new TextFormatting
                {
                    IsBold = _formatting.IsBold,
                    IsItalic = _formatting.IsItalic,
                    IsUnderlined = _formatting.IsUnderlined,
                    FontSize = _formatting.FontSize,
                    FontName = _formatting.FontName
                });
        }

        // Restaure l'état à partir d'un memento
        public void RestoreFromMemento(EditorMemento memento)
        {
            _content = memento.Content;
            _cursorPosition = memento.CursorPosition;
            _selectedText = memento.SelectedText;
            _formatting = new TextFormatting
            {
                IsBold = memento.Formatting.IsBold,
                IsItalic = memento.Formatting.IsItalic,
                IsUnderlined = memento.Formatting.IsUnderlined,
                FontSize = memento.Formatting.FontSize,
                FontName = memento.Formatting.FontName
            };
        }
    }

    /// <summary>
    /// Classe représentant le formatage du texte
    /// </summary>
    public class TextFormatting
    {
        public bool IsBold { get; set; }
        public bool IsItalic { get; set; }
        public bool IsUnderlined { get; set; }
        public int FontSize { get; set; } = 12;
        public string FontName { get; set; } = "Arial";

        public override string ToString()
        {
            return $"{FontName}, {FontSize}pt" +
                   $"{(IsBold ? ", Gras" : "")}" +
                   $"{(IsItalic ? ", Italique" : "")}" +
                   $"{(IsUnderlined ? ", Souligné" : "")}";
        }
    }

    /// <summary>
    /// Memento - Classe qui stocke l'état interne de l'Originator
    /// </summary>
    public class EditorMemento
    {
        public string Content { get; }
        public int CursorPosition { get; }
        public string SelectedText { get; }
        public TextFormatting Formatting { get; }
        public DateTime SavedAt { get; }

        public EditorMemento(string content, int cursorPosition, string selectedText, TextFormatting formatting)
        {
            Content = content;
            CursorPosition = cursorPosition;
            SelectedText = selectedText;
            Formatting = formatting;
            SavedAt = DateTime.Now;
        }
    }

    /// <summary>
    /// Caretaker - Gère et conserve les mementos
    /// </summary>
    public class EditorHistory
    {
        private readonly List<EditorMemento> _mementos = new List<EditorMemento>();
        private int _currentIndex = -1;

        public void SaveState(TextEditor editor)
        {
            // Si nous sommes au milieu de l'historique et que nous faisons une nouvelle action,
            // nous supprimons tout ce qui se trouve après l'index courant
            if (_currentIndex < _mementos.Count - 1)
            {
                _mementos.RemoveRange(_currentIndex + 1, _mementos.Count - _currentIndex - 1);
            }

            _mementos.Add(editor.CreateMemento());
            _currentIndex = _mementos.Count - 1;

            Console.WriteLine($"État sauvegardé à {DateTime.Now:HH:mm:ss.fff}");
        }

        public void Undo(TextEditor editor)
        {
            if (_currentIndex <= 0)
            {
                Console.WriteLine("Impossible d'annuler - début de l'historique atteint");
                return;
            }

            _currentIndex--;
            EditorMemento memento = _mementos[_currentIndex];
            editor.RestoreFromMemento(memento);

            Console.WriteLine($"Annulation effectuée - restauration de l'état sauvegardé à {memento.SavedAt:HH:mm:ss.fff}");
        }

        public void Redo(TextEditor editor)
        {
            if (_currentIndex >= _mementos.Count - 1)
            {
                Console.WriteLine("Impossible de rétablir - fin de l'historique atteinte");
                return;
            }

            _currentIndex++;
            EditorMemento memento = _mementos[_currentIndex];
            editor.RestoreFromMemento(memento);

            Console.WriteLine($"Rétablissement effectué - restauration de l'état sauvegardé à {memento.SavedAt:HH:mm:ss.fff}");
        }

        public void ShowHistory()
        {
            Console.WriteLine("\n--- Historique des états ---");
            for (int i = 0; i < _mementos.Count; i++)
            {
                string marker = i == _currentIndex ? " (état actuel)" : "";
                Console.WriteLine($"{i + 1}. {_mementos[i].SavedAt:HH:mm:ss.fff} - \"{_mementos[i].Content}\"{marker}");
            }
            Console.WriteLine("-------------------------\n");
        }
    }
}


