using System;

namespace Patterns.Creational.Prototype
{
    /// <summary>
    /// Test du pattern Prototype
    /// </summary>
    public class PrototypeTest : Patterns.Creational.IPatternTest
    {
        public void Run()
        {
            Console.WriteLine("Démonstration du pattern Prototype");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Le pattern Prototype permet de créer de nouveaux objets en copiant des objets existants, évitant ainsi de dépendre de leurs classes.");
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

        private void RunPatternDemo()
        {
            // Initialisation du registre de prototypes
            var registry = new ShapeRegistry();

            // Création et enregistrement des prototypes originaux
            var redCircle = new Circle { X = 10, Y = 10, Color = "Rouge", Radius = 20 };
            var blueRectangle = new Rectangle { X = 20, Y = 20, Color = "Bleu", Width = 30, Height = 40 };

            registry.Register("CircleRouge", redCircle);
            registry.Register("RectangleBleu", blueRectangle);

            // Création de copies à partir des prototypes
            Console.WriteLine("1. Clonage des formes depuis le registre :");
            var clonedCircle = (Circle)registry.GetShape("CircleRouge").Clone();
            var clonedRectangle = (Rectangle)registry.GetShape("RectangleBleu").Clone();

            Console.WriteLine("Forme originale: " + redCircle);
            Console.WriteLine("Clone: " + clonedCircle);
            Console.WriteLine();

            Console.WriteLine("Forme originale: " + blueRectangle);
            Console.WriteLine("Clone: " + clonedRectangle);

            // Modification d'un clone pour montrer l'indépendance
            Console.WriteLine("\n2. Modification d'un clone :");
            clonedCircle.X = 100;
            clonedCircle.Color = "Vert";
            
            Console.WriteLine("Original (inchangé): " + redCircle);
            Console.WriteLine("Clone modifié: " + clonedCircle);

            Console.WriteLine("\nAvantages du pattern Prototype :");
            Console.WriteLine("- Création d'objets sans couplage avec leurs classes");
            Console.WriteLine("- Ajout/suppression de produits à l'exécution");
            Console.WriteLine("- Spécification de nouveaux objets par valeurs variables");
            Console.WriteLine("- Réduction du nombre de sous-classes");
            Console.WriteLine("- Configuration dynamique d'une application");
        }

        public string GetName()
        {
            return "Prototype";
        }

        public string GetDescription()
        {
            return "Le pattern Prototype permet de créer de nouveaux objets en copiant des objets existants, évitant ainsi de dépendre de leurs classes.";
        }
    }
}

