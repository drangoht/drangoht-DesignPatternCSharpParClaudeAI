using System;
using System.Collections.Generic;

namespace Patterns.Structural.Flyweight
{
    /// <summary>
    /// Flyweight - CharacterFormat
    /// Contient l'état intrinsèque partagé (police, taille, style)
    /// </summary>
    public class CharacterFormat
    {
        public string FontFamily { get; }
        public int FontSize { get; }
        public string FontStyle { get; } // "normal", "bold", "italic"
        public string Color { get; }

        public CharacterFormat(string fontFamily, int fontSize, string fontStyle, string color)
        {
            FontFamily = fontFamily;
            FontSize = fontSize;
            FontStyle = fontStyle;
            Color = color;
        }

        public void RenderCharacter(char symbol, int posX, int posY)
        {
            Console.WriteLine($"Caractère '{symbol}' rendu en {FontFamily} {FontSize}pt {FontStyle} {Color} à la position ({posX}, {posY})");
        }
    }

    /// <summary>
    /// FlyweightFactory - CharacterFormatFactory
    /// Gère le pool de formats de caractères partagés
    /// </summary>
    public class CharacterFormatFactory
    {
        private readonly Dictionary<string, CharacterFormat> _formats;

        public CharacterFormatFactory()
        {
            _formats = new Dictionary<string, CharacterFormat>();
        }

        public CharacterFormat GetFormat(string fontFamily, int fontSize, string fontStyle, string color)
        {
            string key = $"{fontFamily}-{fontSize}-{fontStyle}-{color}";

            if (!_formats.ContainsKey(key))
            {
                _formats[key] = new CharacterFormat(fontFamily, fontSize, fontStyle, color);
            }

            return _formats[key];
        }

        public int GetFormatCount()
        {
            return _formats.Count;
        }
    }

    /// <summary>
    /// Client - TextEditor
    /// Utilise les flyweights pour rendre du texte
    /// </summary>
    public class TextEditor
    {
        private readonly List<(char Symbol, int PosX, int PosY, CharacterFormat Format)> _characters;
        private readonly CharacterFormatFactory _formatFactory;

        public TextEditor(CharacterFormatFactory formatFactory)
        {
            _characters = new List<(char Symbol, int PosX, int PosY, CharacterFormat Format)>();
            _formatFactory = formatFactory;
        }

        public void AddCharacter(char symbol, int posX, int posY, 
            string fontFamily, int fontSize, string fontStyle, string color)
        {
            var format = _formatFactory.GetFormat(fontFamily, fontSize, fontStyle, color);
            _characters.Add((symbol, posX, posY, format));
        }

        public void RenderText()
        {
            foreach (var (symbol, posX, posY, format) in _characters)
            {
                format.RenderCharacter(symbol, posX, posY);
            }
        }

        public int GetCharacterCount()
        {
            return _characters.Count;
        }
    }
}
