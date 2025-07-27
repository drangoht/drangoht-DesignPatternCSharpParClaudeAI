using System;

namespace Patterns.Structural.Facade
{
    /// <summary>
    /// Subsystem - MediaConverter
    /// </summary>
    public class MediaConverter
    {
        public void ConvertVideo(string inputFile, string outputFormat, string resolution)
        {
            Console.WriteLine($"Conversion vidéo de {inputFile} au format {outputFormat} (résolution: {resolution})...");
            // Simule le processus de conversion
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Conversion vidéo terminée.");
        }

        public void ConvertAudio(string inputFile, string outputFormat, int quality)
        {
            Console.WriteLine($"Conversion audio de {inputFile} au format {outputFormat} (qualité: {quality}%)...");
            // Simule le processus de conversion
            System.Threading.Thread.Sleep(500);
            Console.WriteLine("Conversion audio terminée.");
        }
    }
}
