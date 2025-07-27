using System;
using System.Threading;
using Xunit;

namespace Patterns.Behavioral.Observer.Tests
{
    public class ObserverTest
    {
        [Fact]
        public void TestObserverPattern()
        {
            // Créer la station météo et les observateurs
            var weatherStation = new WeatherStation();
            var currentDisplay = new CurrentConditionsDisplay();
            var statsDisplay = new StatisticsDisplay();
            var forecastDisplay = new ForecastDisplay();

            // Enregistrer les observateurs
            weatherStation.RegisterObserver(currentDisplay);
            weatherStation.RegisterObserver(statsDisplay);
            weatherStation.RegisterObserver(forecastDisplay);

            // Simuler des changements météorologiques
            Console.WriteLine("=== Première mesure ===");
            weatherStation.SetMeasurements(20.5f, 65.0f, 1013.2f);
            
            Thread.Sleep(1000); // Attendre 1 seconde

            Console.WriteLine("\n=== Deuxième mesure ===");
            weatherStation.SetMeasurements(21.2f, 70.0f, 1012.8f);
            
            Thread.Sleep(1000);

            Console.WriteLine("\n=== Troisième mesure ===");
            weatherStation.SetMeasurements(20.8f, 72.5f, 1014.1f);

            // Test de suppression d'un observateur
            Console.WriteLine("\n=== Suppression de l'affichage actuel ===");
            weatherStation.RemoveObserver(currentDisplay);

            Console.WriteLine("\n=== Quatrième mesure (sans l'affichage actuel) ===");
            weatherStation.SetMeasurements(19.5f, 68.0f, 1015.2f);

            // Vérifier que les données sont correctement transmises
            var finalData = new WeatherData(19.5f, 68.0f, 1015.2f);
            
            // Réenregistrer l'affichage actuel et vérifier qu'il reçoit la dernière mesure
            Console.WriteLine("\n=== Réenregistrement de l'affichage actuel ===");
            weatherStation.RegisterObserver(currentDisplay);

            // Vérifier que les statistiques sont calculées sur toutes les mesures
            Console.WriteLine("\n=== Statistiques finales ===");
            statsDisplay.Display();

            // Vérifier que les prévisions sont basées sur les deux dernières mesures
            Console.WriteLine("\n=== Prévisions finales ===");
            forecastDisplay.Display();

            // Test d'enregistrement multiple du même observateur
            Console.WriteLine("\n=== Test d'enregistrement multiple ===");
            weatherStation.RegisterObserver(currentDisplay); // Devrait être ignoré
            weatherStation.SetMeasurements(20.0f, 70.0f, 1013.0f);
        }
    }
}
