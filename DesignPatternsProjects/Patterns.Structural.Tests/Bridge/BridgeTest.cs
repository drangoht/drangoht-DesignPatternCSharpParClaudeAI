using System;
using Xunit;

namespace Patterns.Structural.Bridge.Tests
{
    public class BridgeTest
    {
        [Fact]
        public void TestBridgePattern()
        {
            // Test avec une TV
            IDevice tv = new TV();
            RemoteControl tvRemote = new RemoteControl(tv);
            AdvancedRemoteControl tvAdvancedRemote = new AdvancedRemoteControl(tv);

            // Test des opérations de base sur la TV
            tvRemote.TogglePower(); // Allume la TV
            tvRemote.VolumeUp(); // Volume à 10%
            tvRemote.VolumeUp(); // Volume à 20%
            tvRemote.ChannelUp(); // Chaîne 1
            tvRemote.DisplayStatus();
            Assert.True(tv.IsEnabled());
            Assert.Equal(20, tv.GetVolume());
            Assert.Equal(1, tv.GetChannel());

            // Test des opérations avancées sur la TV
            tvAdvancedRemote.Mute(); // Volume à 0%
            Assert.Equal(0, tv.GetVolume());
            tvAdvancedRemote.Unmute(); // Volume retourne à 20%
            Assert.Equal(20, tv.GetVolume());

            // Test avec une Radio
            IDevice radio = new Radio();
            RemoteControl radioRemote = new RemoteControl(radio);
            AdvancedRemoteControl radioAdvancedRemote = new AdvancedRemoteControl(radio);

            // Test des opérations de base sur la Radio
            radioRemote.TogglePower(); // Allume la Radio
            radioRemote.VolumeUp(); // Volume à 10%
            radioRemote.VolumeUp(); // Volume à 20%
            radioRemote.ChannelUp(); // Fréquence 1.0 MHz
            radioRemote.DisplayStatus();
            Assert.True(radio.IsEnabled());
            Assert.Equal(20, radio.GetVolume());
            Assert.Equal(1, radio.GetChannel());

            // Test des opérations avancées sur la Radio
            radioAdvancedRemote.Mute(); // Volume à 0%
            Assert.Equal(0, radio.GetVolume());
            radioAdvancedRemote.Unmute(); // Volume retourne à 20%
            Assert.Equal(20, radio.GetVolume());
        }
    }
}
