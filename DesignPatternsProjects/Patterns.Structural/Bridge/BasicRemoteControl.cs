using System;

namespace Patterns.Structural.Bridge
{
    /// <summary>
    /// Télécommande basique concrète
    /// </summary>
    public class BasicRemoteControl : RemoteControl
    {
        public BasicRemoteControl(IDevice device) : base(device)
        {
        }
    }
}
