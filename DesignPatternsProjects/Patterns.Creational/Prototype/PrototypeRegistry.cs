using System;
using System.Collections.Generic;

namespace Patterns.Creational.Prototype
{
    /// <summary>
    /// Registre de prototypes qui stocke des prototypes pré-définis
    /// </summary>
    public class ShapeRegistry
    {
        private readonly Dictionary<string, Shape> _shapes = new();

        /// <summary>
        /// Initialise le registre avec des formes par défaut
        /// </summary>
        public void InitializeDefault()
        {
            var circle = new Circle
            {
                X = 10,
                Y = 10,
                Radius = 20,
                Color = "Rouge"
            };
            _shapes["CircleRouge"] = circle;

            var rectangle = new Rectangle
            {
                X = 20,
                Y = 20,
                Width = 30,
                Height = 10,
                Color = "Bleu"
            };
            _shapes["RectangleBleu"] = rectangle;
        }

        /// <summary>
        /// Enregistre une forme prototype
        /// </summary>
        public void Register(string key, Shape shape)
        {
            _shapes[key] = shape;
        }

        /// <summary>
        /// Récupère et clone une forme du registre
        /// </summary>
        public Shape GetShape(string key)
        {
            if (!_shapes.ContainsKey(key))
            {
                throw new ArgumentException($"Forme '{key}' non trouvée dans le registre.");
            }
            
            // Retourne un clone du prototype, pas l'original
            return (Shape)_shapes[key].Clone();
        }

        /// <summary>
        /// Liste tous les prototypes disponibles dans le registre
        /// </summary>
        public void ListAvailableShapes()
        {
            Console.WriteLine("Formes disponibles dans le registre:");
            foreach (var key in _shapes.Keys)
            {
                Console.WriteLine($"- {key}: {_shapes[key]}");
            }
        }
    }
}