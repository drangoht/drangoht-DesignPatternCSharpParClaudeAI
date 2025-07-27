using System;

namespace Patterns.Structural.Adapter
{
    /// <summary>
    /// Test du pattern Adapter
    /// </summary>
    public class AdapterTest : Patterns.Structural.IPatternTest
    {
        public void Run()
        {
            Console.WriteLine("Démonstration du pattern Adapter");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Le pattern Adapter permet à des interfaces incompatibles de travailler ensemble en convertissant l'interface d'une classe en une autre attendue par les clients.");
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
            // Crée un lecteur audio standard
            AudioPlayer audioPlayer = new AudioPlayer();

            // Test avec différents formats de fichiers
            Console.WriteLine("1. Test avec un fichier MP3 (format natif):");
            audioPlayer.Play("musique.mp3");

            Console.WriteLine("\n2. Test avec un fichier VLC (via adaptateur):");
            audioPlayer.Play("video.vlc");

            Console.WriteLine("\n3. Test avec un fichier MP4 (via adaptateur):");
            audioPlayer.Play("film.mp4");

            Console.WriteLine("\n4. Test avec un format non supporté:");
            audioPlayer.Play("document.pdf");
        }

        public string GetName()
        {
            return "Adapter";
        }

        public string GetDescription()
        {
            return "Le pattern Adapter permet à des interfaces incompatibles de travailler ensemble en convertissant l'interface d'une classe en une autre attendue par les clients.";
        }
    }
}

