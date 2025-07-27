using System;

namespace Patterns.Structural.Bridge
{
    /// <summary>
    /// Abstraction qui définit l'interface pour le "côté contrôle" du bridge
    /// </summary>
    public abstract class RemoteControl
    {
        // Référence à l'objet d'implémentation
        protected IDevice device;

        public RemoteControl(IDevice device)
        {
            this.device = device;
        }

        public void TogglePower()
        {
            if (device.IsEnabled())
            {
                device.Disable();
            }
            else
            {
                device.Enable();
            }
        }

        public void VolumeDown()
        {
            device.SetVolume(device.GetVolume() - 10);
        }

        public void VolumeUp()
        {
            device.SetVolume(device.GetVolume() + 10);
        }

        public void ChannelDown()
        {
            device.SetChannel(device.GetChannel() - 1);
        }

        public void ChannelUp()
        {
            device.SetChannel(device.GetChannel() + 1);
        }
    }

    /// <summary>
    /// Abstraction raffinée (extension de l'abstraction de base)
    /// </summary>
    public class AdvancedRemoteControl : RemoteControl
    {
        public AdvancedRemoteControl(IDevice device) : base(device)
        {
        }

        public void Mute()
        {
            device.SetVolume(0);
            Console.WriteLine("Mode muet activé");
        }

        public void SetChannel(int channel)
        {
            device.SetChannel(channel);
        }
    }
}


