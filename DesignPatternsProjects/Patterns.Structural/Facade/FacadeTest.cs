using System;

namespace Patterns.Structural.Facade
{
    /// <summary>
    /// Test du pattern Facade
    /// </summary>
    public class FacadeTest : Patterns.Structural.IPatternTest
    {
        public void Run()
        {
            Console.WriteLine("Démonstration du pattern Facade");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Le pattern Facade fournit une interface unifiée à un ensemble d'interfaces dans un sous-système.");
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
            // Créer une instance de la façade
            var converter = new MediaConverter();

            Console.WriteLine("1. Conversion d'une vidéo:");
            // La façade cache la complexité des sous-systèmes
            converter.ConvertVideo("movie.avi", "mp4", "1920x1080");

            Console.WriteLine("2. Conversion d'un fichier audio:");
            // Un autre exemple d'utilisation de la façade
            converter.ConvertAudio("song.wav", "mp3", 85);
        }

        public string GetName()
        {
            return "Facade";
        }

        public string GetDescription()
        {
            return "Le pattern Facade fournit une interface unifiée à un ensemble d'interfaces dans un sous-système.";
        }
    }
}

