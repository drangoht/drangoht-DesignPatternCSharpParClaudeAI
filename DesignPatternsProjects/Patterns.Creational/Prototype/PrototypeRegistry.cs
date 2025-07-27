using System;
using System.Collections.Generic;

namespace Patterns.Creational.Prototype
{
    /// <summary>
    /// Registre de prototypes qui stocke des prototypes pré-définis
    /// </summary>
    public class ShapeRegistry
    {
        private Dictionary<string, Shape> shapes = new Dictionary<string, Shape>();

        /// <summary>
        /// Initialise le registre avec des formes par défaut
        /// </summary>
        public ShapeRegistry()
        {
            // Initialiser avec des formes par défaut
            Circle circle = new Circle();
            circle.X = 10;
            circle.Y = 10;
            circle.Radius = 20;
            circle.Color = "Rouge";
            shapes["CircleRouge"] = circle;

            Rectangle rectangle = new Rectangle();
            rectangle.X = 20;
            rectangle.Y = 20;
            rectangle.Width = 30;
            rectangle.Height = 10;
            rectangle.Color = "Bleu";
            shapes["RectangleBleu"] = rectangle;
        }

        /// <summary>
        /// Ajoute une forme au registre
        /// </summary>
        public void RegisterShape(string key, Shape shape)
        {
            shapes[key] = shape;
        }

        /// <summary>
        /// Récupère et clone une forme du registre
        /// </summary>
        public Shape GetShape(string key)
        {
            if (!shapes.ContainsKey(key))
            {
                throw new ArgumentException($"Forme '{key}' non trouvée dans le registre.");
            }
            
            // Retourne un clone du prototype, pas l'original
            return (Shape)shapes[key].Clone();
        }

        /// <summary>
        /// Liste tous les prototypes disponibles dans le registre
        /// </summary>
        public void ListAvailableShapes()
        {
            Console.WriteLine("Formes disponibles dans le registre:");
            foreach (var key in shapes.Keys)
            {
                Console.WriteLine($"- {key}: {shapes[key]}");
            }
        }
    }
}


