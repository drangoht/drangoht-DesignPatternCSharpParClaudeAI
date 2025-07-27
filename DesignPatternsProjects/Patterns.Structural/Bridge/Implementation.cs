using System;

namespace Patterns.Structural.Bridge
{
    /// <summary>
    /// Interface d'implémentation qui définit l'interface pour les classes d'implémentation concrètes.
    /// C'est le "côté implémentation" du bridge.
    /// </summary>
    public interface IDevice
    {
        bool IsEnabled();
        void Enable();
        void Disable();
        int GetVolume();
        void SetVolume(int percent);
        int GetChannel();
        void SetChannel(int channel);
    }

    /// <summary>
    /// Implémentation concrète pour une télévision
    /// </summary>
    public class TV : IDevice
    {
        private bool _enabled = false;
        private int _volume = 30;
        private int _channel = 1;

        public bool IsEnabled()
        {
            return _enabled;
        }

        public void Enable()
        {
            _enabled = true;
            Console.WriteLine("TV: Activée");
        }

        public void Disable()
        {
            _enabled = false;
            Console.WriteLine("TV: Désactivée");
        }

        public int GetVolume()
        {
            return _volume;
        }

        public void SetVolume(int percent)
        {
            if (percent > 100) percent = 100;
            if (percent < 0) percent = 0;
            _volume = percent;
            Console.WriteLine($"TV: Volume réglé à {percent}%");
        }

        public int GetChannel()
        {
            return _channel;
        }

        public void SetChannel(int channel)
        {
            _channel = channel;
            Console.WriteLine($"TV: Chaîne changée pour {channel}");
        }
    }

    /// <summary>
    /// Implémentation concrète pour une radio
    /// </summary>
    public class Radio : IDevice
    {
        private bool _enabled = false;
        private int _volume = 20;
        private int _channel = 1;

        public bool IsEnabled()
        {
            return _enabled;
        }

        public void Enable()
        {
            _enabled = true;
            Console.WriteLine("Radio: Activée");
        }

        public void Disable()
        {
            _enabled = false;
            Console.WriteLine("Radio: Désactivée");
        }

        public int GetVolume()
        {
            return _volume;
        }

        public void SetVolume(int percent)
        {
            if (percent > 100) percent = 100;
            if (percent < 0) percent = 0;
            _volume = percent;
            Console.WriteLine($"Radio: Volume réglé à {percent}%");
        }

        public int GetChannel()
        {
            return _channel;
        }

        public void SetChannel(int channel)
        {
            _channel = channel;
            Console.WriteLine($"Radio: Station changée pour {channel} FM");
        }
    }
}


