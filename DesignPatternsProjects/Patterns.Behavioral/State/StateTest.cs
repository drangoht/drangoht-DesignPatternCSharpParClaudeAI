using System;

namespace Patterns.Behavioral.State
{
    /// <summary>
    /// Test du pattern State
    /// </summary>
    public class StateTest : Patterns.Behavioral.IPatternTest
    {
        public void Run()
        {
            Console.WriteLine("Démonstration du pattern State");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Le pattern State permet à un objet de modifier son comportement lorsque son état interne change.");
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
            // Création du contexte (lecteur audio)
            Console.WriteLine("Initialisation du lecteur audio...");
            AudioPlayer player = new AudioPlayer();
            Console.WriteLine(player.GetStatus());
            Console.WriteLine();

            // Test des transitions d'état
            Console.WriteLine("1. Test des commandes depuis l'état 'Arrêté':");
            Console.WriteLine("-----------------------------------------");
            player.Play();  // Arrêté -> Lecture
            Console.WriteLine(player.GetStatus());
            
            player.NextTrack();
            Console.WriteLine(player.GetStatus());
            
            player.VolumeUp();
            Console.WriteLine();
            
            Console.WriteLine("2. Test des commandes depuis l'état 'Lecture':");
            Console.WriteLine("-------------------------------------------");
            player.Pause(); // Lecture -> Pause
            Console.WriteLine(player.GetStatus());
            
            Console.WriteLine("3. Test des commandes depuis l'état 'Pause':");
            Console.WriteLine("-----------------------------------------");
            player.Play();  // Pause -> Lecture
            Console.WriteLine(player.GetStatus());
            
            player.Stop();  // Lecture -> Arrêté
            Console.WriteLine(player.GetStatus());
            Console.WriteLine();
            
            Console.WriteLine("4. Test du verrouillage du lecteur:");
            Console.WriteLine("-------------------------------");
            player.Play();  // Arrêté -> Lecture
            player.Lock();  // Lecture -> Verrouillé
            Console.WriteLine(player.GetStatus());
            
            // Ces commandes ne doivent pas avoir d'effet dans l'état verrouillé
            Console.WriteLine("\nTentatives d'actions en état verrouillé:");
            player.Play();
            player.Stop();
            player.NextTrack();
            
            player.Unlock(); // Verrouillé -> Retour à l'état précédent (Lecture)
            Console.WriteLine(player.GetStatus());
            Console.WriteLine();
            
            Console.WriteLine("Le pattern State permet à un objet de modifier son comportement");
            Console.WriteLine("quand son état interne change, donnant l'impression que l'objet");
            Console.WriteLine("a changé de classe. Cela permet d'éviter les grandes structures");
            Console.WriteLine("conditionnelles dans le code du contexte.");
        }

        public string GetName()
        {
            return "State";
        }

        public string GetDescription()
        {
            return "Le pattern State permet à un objet de modifier son comportement lorsque son état interne change.";
        }
    }
}

