using System;

namespace Patterns.Structural.Bridge
{
    /// <summary>
    /// Test du pattern Bridge
    /// </summary>
    public class BridgeTest : Patterns.Structural.IPatternTest
    {
        public void Run()
        {
            Console.WriteLine("Démonstration du pattern Bridge");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Le pattern Bridge sépare une abstraction de son implémentation afin que les deux puissent varier indépendamment.");
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
            Console.WriteLine("1. Test avec TV et télécommande basique:");
            IDevice tv = new TV();
            BasicRemoteControl basicRemote = new BasicRemoteControl(tv);
            
            basicRemote.TogglePower();
            basicRemote.ChannelUp();
            basicRemote.VolumeUp();
            basicRemote.VolumeUp();
            basicRemote.TogglePower();
            
            Console.WriteLine("\n2. Test avec Radio et télécommande avancée:");
            IDevice radio = new Radio();
            AdvancedRemoteControl advancedRemote = new AdvancedRemoteControl(radio);
            
            advancedRemote.TogglePower();
            advancedRemote.SetChannel(7);
            advancedRemote.VolumeUp();
            advancedRemote.Mute();
            advancedRemote.TogglePower();
            
            Console.WriteLine("\nLe pattern Bridge a permis de changer d'appareils (TV, Radio) et de télécommandes (basique, avancée)");
            Console.WriteLine("sans modifier le code client, démontrant la séparation entre abstraction et implémentation.");
        }

        public string GetName()
        {
            return "Bridge";
        }

        public string GetDescription()
        {
            return "Le pattern Bridge sépare une abstraction de son implémentation afin que les deux puissent varier indépendamment.";
        }
    }
}

