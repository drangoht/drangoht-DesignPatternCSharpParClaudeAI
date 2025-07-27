using System;
using System.Collections.Generic;

namespace Patterns.Structural.Facade
{
    // Sous-systèmes complexes

    /// <summary>
    /// Sous-système pour la gestion des codecs audio
    /// </summary>
    public class AudioSystem
    {
        private readonly Dictionary<string, string> _codecs = new()
        {
            { "mp3", "MPEG Layer-3" },
            { "aac", "Advanced Audio Coding" },
            { "wav", "Waveform Audio Format" }
        };

        public void ExtractAudio(string file)
        {
            Console.WriteLine($"Extraction de l'audio depuis {file}");
        }

        public void ConvertAudio(string sourceFormat, string targetFormat)
        {
            var sourceCodec = _codecs.GetValueOrDefault(sourceFormat, "Unknown");
            var targetCodec = _codecs.GetValueOrDefault(targetFormat, "Unknown");
            Console.WriteLine($"Conversion audio de {sourceCodec} vers {targetCodec}");
        }

        public void AdjustVolume(int level)
        {
            Console.WriteLine($"Ajustement du volume à {level}%");
        }
    }

    /// <summary>
    /// Sous-système pour la gestion des codecs vidéo
    /// </summary>
    public class VideoSystem
    {
        private readonly Dictionary<string, string> _codecs = new()
        {
            { "mp4", "MPEG-4" },
            { "avi", "Audio Video Interleave" },
            { "mkv", "Matroska Video" }
        };

        public void ExtractVideo(string file)
        {
            Console.WriteLine($"Extraction de la vidéo depuis {file}");
        }

        public void ConvertVideo(string sourceFormat, string targetFormat)
        {
            var sourceCodec = _codecs.GetValueOrDefault(sourceFormat, "Unknown");
            var targetCodec = _codecs.GetValueOrDefault(targetFormat, "Unknown");
            Console.WriteLine($"Conversion vidéo de {sourceCodec} vers {targetCodec}");
        }

        public void AdjustResolution(string resolution)
        {
            Console.WriteLine($"Ajustement de la résolution à {resolution}");
        }
    }

    /// <summary>
    /// Sous-système pour l'exportation des fichiers
    /// </summary>
    public class ExportSystem
    {
        public void SaveToFile(string filename)
        {
            Console.WriteLine($"Sauvegarde du fichier vers {filename}");
        }

        public void CompressFile(string filename)
        {
            Console.WriteLine($"Compression du fichier {filename}");
        }
    }

    /// <summary>
    /// Façade - fournit une interface unifiée aux sous-systèmes
    /// </summary>
    public class MediaConverter
    {
        private readonly AudioSystem _audioSystem;
        private readonly VideoSystem _videoSystem;
        private readonly ExportSystem _exportSystem;

        public MediaConverter()
        {
            _audioSystem = new AudioSystem();
            _videoSystem = new VideoSystem();
            _exportSystem = new ExportSystem();
        }

        public void ConvertVideo(string sourceFile, string targetFormat, string resolution)
        {
            Console.WriteLine("\nDémarrage de la conversion vidéo...");
            _videoSystem.ExtractVideo(sourceFile);
            _videoSystem.ConvertVideo(GetFileExtension(sourceFile), targetFormat);
            _videoSystem.AdjustResolution(resolution);
            _exportSystem.SaveToFile($"video.{targetFormat}");
            _exportSystem.CompressFile($"video.{targetFormat}");
            Console.WriteLine("Conversion vidéo terminée.\n");
        }

        public void ConvertAudio(string sourceFile, string targetFormat, int volume)
        {
            Console.WriteLine("\nDémarrage de la conversion audio...");
            _audioSystem.ExtractAudio(sourceFile);
            _audioSystem.ConvertAudio(GetFileExtension(sourceFile), targetFormat);
            _audioSystem.AdjustVolume(volume);
            _exportSystem.SaveToFile($"audio.{targetFormat}");
            _exportSystem.CompressFile($"audio.{targetFormat}");
            Console.WriteLine("Conversion audio terminée.\n");
        }

        private string GetFileExtension(string filename)
        {
            var parts = filename.Split('.');
            return parts.Length > 1 ? parts[^1].ToLower() : "unknown";
        }
    }
}
