using System;

namespace Patterns.Structural.Facade
{
    /// <summary>
    /// Subsystem - TV
    /// </summary>
    public class TV
    {
        public void TurnOn()
        {
            Console.WriteLine("TV allumée");
        }

        public void TurnOff()
        {
            Console.WriteLine("TV éteinte");
        }

        public void SelectInput(string input)
        {
            Console.WriteLine($"TV entrée réglée sur: {input}");
        }
    }

    /// <summary>
    /// Subsystem - DVDPlayer
    /// </summary>
    public class DVDPlayer
    {
        public void TurnOn()
        {
            Console.WriteLine("Lecteur DVD allumé");
        }

        public void TurnOff()
        {
            Console.WriteLine("Lecteur DVD éteint");
        }

        public void Play()
        {
            Console.WriteLine("Lecture DVD");
        }

        public void Stop()
        {
            Console.WriteLine("Arrêt DVD");
        }

        public void Eject()
        {
            Console.WriteLine("Éjection DVD");
        }
    }

    /// <summary>
    /// Subsystem - AudioSystem
    /// </summary>
    public class AudioSystem
    {
        private int _volume;

        public void TurnOn()
        {
            Console.WriteLine("Système audio allumé");
            _volume = 10; // Volume par défaut
        }

        public void TurnOff()
        {
            Console.WriteLine("Système audio éteint");
        }

        public void SetVolume(int level)
        {
            if (level < 0) level = 0;
            if (level > 100) level = 100;
            _volume = level;
            Console.WriteLine($"Volume audio réglé à {_volume}");
        }

        public void SelectInput(string input)
        {
            Console.WriteLine($"Entrée audio réglée sur: {input}");
        }

        public int GetVolume()
        {
            return _volume;
        }
    }

    /// <summary>
    /// Subsystem - Lights
    /// </summary>
    public class Lights
    {
        private int _brightness;

        public void TurnOn()
        {
            SetBrightness(100);
        }

        public void TurnOff()
        {
            SetBrightness(0);
        }

        public void Dim(int percent)
        {
            SetBrightness(percent);
        }

        private void SetBrightness(int percent)
        {
            if (percent < 0) percent = 0;
            if (percent > 100) percent = 100;
            _brightness = percent;
            Console.WriteLine($"Lumières réglées à {_brightness}%");
        }

        public int GetBrightness()
        {
            return _brightness;
        }
    }

    /// <summary>
    /// Facade - HomeTheaterFacade
    /// </summary>
    public class HomeTheaterFacade
    {
        private readonly TV _tv;
        private readonly DVDPlayer _dvd;
        private readonly AudioSystem _audio;
        private readonly Lights _lights;

        public HomeTheaterFacade(TV tv, DVDPlayer dvd, AudioSystem audio, Lights lights)
        {
            _tv = tv;
            _dvd = dvd;
            _audio = audio;
            _lights = lights;
        }

        public void WatchMovie()
        {
            Console.WriteLine("=== Démarrage du film ===");
            _lights.Dim(20);
            _tv.TurnOn();
            _tv.SelectInput("DVD");
            _dvd.TurnOn();
            _audio.TurnOn();
            _audio.SelectInput("DVD");
            _audio.SetVolume(40);
            _dvd.Play();
        }

        public void EndMovie()
        {
            Console.WriteLine("=== Fin du film ===");
            _dvd.Stop();
            _dvd.Eject();
            _dvd.TurnOff();
            _audio.TurnOff();
            _tv.TurnOff();
            _lights.TurnOn();
        }

        // Pour les tests
        public (TV tv, DVDPlayer dvd, AudioSystem audio, Lights lights) GetComponents()
        {
            return (_tv, _dvd, _audio, _lights);
        }
    }
}
