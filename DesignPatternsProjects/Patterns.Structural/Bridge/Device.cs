using System;

namespace Patterns.Structural.Bridge
{
    /// <summary>
    /// Implementation - Interface Device
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
        void DisplayStatus();
    }

    /// <summary>
    /// Implementation concrète - TV
    /// </summary>
    public class TV : IDevice
    {
        private bool _isEnabled;
        private int _volume;
        private int _channel;

        public bool IsEnabled()
        {
            return _isEnabled;
        }

        public void Enable()
        {
            _isEnabled = true;
            Console.WriteLine("TV allumée");
        }

        public void Disable()
        {
            _isEnabled = false;
            Console.WriteLine("TV éteinte");
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
            Console.WriteLine($"TV volume: {_volume}%");
        }

        public int GetChannel()
        {
            return _channel;
        }

        public void SetChannel(int channel)
        {
            _channel = channel;
            Console.WriteLine($"TV chaîne: {_channel}");
        }

        public void DisplayStatus()
        {
            Console.WriteLine($"TV [On: {_isEnabled}, Volume: {_volume}%, Chaîne: {_channel}]");
        }
    }

    /// <summary>
    /// Implementation concrète - Radio
    /// </summary>
    public class Radio : IDevice
    {
        private bool _isEnabled;
        private int _volume;
        private int _channel;

        public bool IsEnabled()
        {
            return _isEnabled;
        }

        public void Enable()
        {
            _isEnabled = true;
            Console.WriteLine("Radio allumée");
        }

        public void Disable()
        {
            _isEnabled = false;
            Console.WriteLine("Radio éteinte");
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
            Console.WriteLine($"Radio volume: {_volume}%");
        }

        public int GetChannel()
        {
            return _channel;
        }

        public void SetChannel(int channel)
        {
            _channel = channel;
            Console.WriteLine($"Radio fréquence: {_channel}.0 MHz");
        }

        public void DisplayStatus()
        {
            Console.WriteLine($"Radio [On: {_isEnabled}, Volume: {_volume}%, Fréquence: {_channel}.0 MHz]");
        }
    }
}
