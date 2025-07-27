using System;

namespace Patterns.Structural.Adapter
{
    /// <summary>
    /// Interface Target - L'interface que le client utilise
    /// </summary>
    public interface IMediaPlayer
    {
        void Play(string filename);
    }

    /// <summary>
    /// Classe Adaptee - La classe avec une interface incompatible
    /// </summary>
    public class AdvancedMediaPlayer
    {
        public void PlayVlc(string filename)
        {
            Console.WriteLine($"Lecture du fichier VLC: {filename}");
        }

        public void PlayMp4(string filename)
        {
            Console.WriteLine($"Lecture du fichier MP4: {filename}");
        }
    }

    /// <summary>
    /// L'Adapter - Convertit l'interface de l'Adaptee en une interface que le client peut utiliser
    /// </summary>
    public class MediaAdapter : IMediaPlayer
    {
        private AdvancedMediaPlayer _advancedMusicPlayer;

        public MediaAdapter()
        {
            _advancedMusicPlayer = new AdvancedMediaPlayer();
        }

        public void Play(string filename)
        {
            // Extrait l'extension du fichier
            string extension = System.IO.Path.GetExtension(filename).ToLower();

            // Adapte l'appel à la méthode appropriée selon le format
            switch (extension)
            {
                case ".vlc":
                    _advancedMusicPlayer.PlayVlc(filename);
                    break;
                case ".mp4":
                    _advancedMusicPlayer.PlayMp4(filename);
                    break;
                default:
                    Console.WriteLine($"Format {extension} non supporté");
                    break;
            }
        }
    }

    /// <summary>
    /// Concrete - Une implémentation basique du lecteur média
    /// </summary>
    public class AudioPlayer : IMediaPlayer
    {
        private MediaAdapter _mediaAdapter;

        public AudioPlayer()
        {
            _mediaAdapter = new MediaAdapter();
        }

        public void Play(string filename)
        {
            // Supporte nativement le format mp3
            string extension = System.IO.Path.GetExtension(filename).ToLower();

            if (extension == ".mp3")
            {
                Console.WriteLine($"Lecture du fichier MP3: {filename}");
            }
            // Utilise l'adaptateur pour les autres formats
            else
            {
                _mediaAdapter.Play(filename);
            }
        }
    }
}
