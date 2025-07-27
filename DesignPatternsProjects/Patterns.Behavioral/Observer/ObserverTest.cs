using System;

namespace Patterns.Behavioral.Observer
{
    /// <summary>
    /// Test du pattern Observer
    /// </summary>
    public class ObserverTest : Patterns.Behavioral.IPatternTest
    {
        public void Run()
        {
            Console.WriteLine("Démonstration du pattern Observer");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Le pattern Observer définit une dépendance un-à-plusieurs entre des objets pour que tous les dépendants soient notifiés des changements.");
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
            // Créer le sujet (station météo)
            Console.WriteLine("Initialisation de la station météo (sujet)...");
            WeatherStation weatherStation = new WeatherStation();

            // Créer les observateurs
            Console.WriteLine("\nCréation des observateurs (écrans d'affichage)...");
            CurrentConditionsDisplay currentDisplay = new CurrentConditionsDisplay("Écran conditions actuelles");
            StatisticsDisplay statisticsDisplay = new StatisticsDisplay("Écran statistiques");
            
            // Attacher les observateurs au sujet
            Console.WriteLine("\nAttachement des observateurs au sujet...");
            weatherStation.Attach(currentDisplay);
            weatherStation.Attach(statisticsDisplay);
            
            // Simuler des changements d'état du sujet
            Console.WriteLine("\nSimulation de changements météorologiques...");
            Console.WriteLine("\n--- Premier relevé ---");
            weatherStation.SetMeasurements(20.5, 65.0, 1013.2);
            
            Console.WriteLine("\n--- Second relevé ---");
            weatherStation.SetMeasurements(22.8, 70.2, 1007.5);
            
            // Démontrer le détachement d'un observateur
            Console.WriteLine("\nDétachement d'un observateur...");
            weatherStation.Detach(currentDisplay);
            
            Console.WriteLine("\n--- Troisième relevé (après détachement) ---");
            weatherStation.SetMeasurements(25.1, 55.8, 1002.1);
            
            Console.WriteLine("\nLe pattern Observer permet une communication loose-coupling entre objets,");
            Console.WriteLine("où le sujet ne connaît pas les détails des observateurs, seulement leur interface.");
        }

        public string GetName()
        {
            return "Observer";
        }

        public string GetDescription()
        {
            return "Le pattern Observer définit une dépendance un-à-plusieurs entre des objets pour que tous les dépendants soient notifiés des changements.";
        }
    }
}

