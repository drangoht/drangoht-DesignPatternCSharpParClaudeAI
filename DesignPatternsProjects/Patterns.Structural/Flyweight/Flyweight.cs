using System;
using System.Collections.Generic;

namespace Patterns.Structural.Flyweight
{
    /// <summary>
    /// Interface Flyweight - Définit l'interface par laquelle les flyweights peuvent recevoir
    /// et agir sur des états extrinsèques
    /// </summary>
    public interface ITextFormatting
    {
        void Display(string text, string extrinsicState);
    }

    /// <summary>
    /// Flyweight Concret - Implémente l'interface Flyweight et stocke l'état intrinsèque
    /// L'état intrinsèque est stocké/partagé par le Flyweight et est indépendant du contexte
    /// </summary>
    public class TextFormat : ITextFormatting
    {
        // État intrinsèque - stocké et partagé par tous les objets utilisant ce flyweight
        private readonly string _fontName;
        private readonly int _fontSize;
        private readonly string _color;

        public TextFormat(string fontName, int fontSize, string color)
        {
            _fontName = fontName;
            _fontSize = fontSize;
            _color = color;
            
            // Simulation de création d'objet coûteuse
            Console.WriteLine($"Création d'un nouveau format de texte: {fontName}, {fontSize}pt, {color}");
        }

        public void Display(string text, string position)
        {
            // Utilisation de l'état intrinsèque (_fontName, _fontSize, _color)
            // et de l'état extrinsèque (text, position) fourni par le client
            Console.WriteLine($"Texte: '{text}' en {_fontName}, {_fontSize}pt, {_color} à la position {position}");
        }
    }

    /// <summary>
    /// FlyweightFactory - Crée et gère les objets flyweight
    /// Assure que les flyweights sont partagés correctement
    /// </summary>
    public class TextFormatFactory
    {
        private readonly Dictionary<string, ITextFormatting> _flyweights = new Dictionary<string, ITextFormatting>();

        /// <summary>
        /// Retourne un Flyweight existant ou en crée un nouveau si nécessaire
        /// </summary>
        public ITextFormatting GetTextFormat(string fontName, int fontSize, string color)
        {
            // Crée une clé basée sur les états intrinsèques
            string key = $"{fontName}_{fontSize}_{color}";
            
            // Retourne un flyweight existant ou en crée un nouveau
            if (!_flyweights.ContainsKey(key))
            {
                Console.WriteLine($"FlyweightFactory: Impossible de trouver le format de texte, création d'un nouveau avec la clé: {key}");
                _flyweights[key] = new TextFormat(fontName, fontSize, color);
            }
            else
            {
                Console.WriteLine($"FlyweightFactory: Réutilisation d'un format existant avec la clé: {key}");
            }
            
            return _flyweights[key];
        }

        public int GetFlyweightCount()
        {
            return _flyweights.Count;
        }
    }
}


