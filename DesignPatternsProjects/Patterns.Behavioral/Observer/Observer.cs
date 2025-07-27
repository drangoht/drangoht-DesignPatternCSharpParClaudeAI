using System;
using System.Collections.Generic;

namespace Patterns.Behavioral.Observer
{
    /// <summary>
    /// Interface Observer - définit comment les objets sont notifiés des changements
    /// </summary>
    public interface IWeatherObserver
    {
        void Update(double temperature, double humidity, double pressure);
        string Name { get; }
    }

    /// <summary>
    /// Interface Subject - définit les méthodes pour gérer les observateurs
    /// </summary>
    public interface IWeatherSubject
    {
        void Attach(IWeatherObserver observer);
        void Detach(IWeatherObserver observer);
        void NotifyObservers();
    }

    /// <summary>
    /// ConcreteSubject - maintient l'état et notifie les observateurs des changements
    /// </summary>
    public class WeatherStation : IWeatherSubject
    {
        private readonly List<IWeatherObserver> _observers;
        private double _temperature;
        private double _humidity;
        private double _pressure;

        public WeatherStation()
        {
            _observers = new List<IWeatherObserver>();
        }

        public void Attach(IWeatherObserver observer)
        {
            Console.WriteLine($"Station météo : Ajout de l'observateur {observer.Name}");
            _observers.Add(observer);
        }

        public void Detach(IWeatherObserver observer)
        {
            Console.WriteLine($"Station météo : Retrait de l'observateur {observer.Name}");
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update(_temperature, _humidity, _pressure);
            }
        }

        // Méthode pour simuler des changements de mesures
        public void SetMeasurements(double temperature, double humidity, double pressure)
        {
            _temperature = temperature;
            _humidity = humidity;
            _pressure = pressure;
            NotifyObservers();
        }
    }

    /// <summary>
    /// ConcreteObserver - réagit aux mises à jour du sujet
    /// </summary>
    public class CurrentConditionsDisplay : IWeatherObserver
    {
        public string Name { get; private set; }

        public CurrentConditionsDisplay(string name)
        {
            Name = name;
        }

        public void Update(double temperature, double humidity, double pressure)
        {
            Console.WriteLine($"\n{Name} : ");
            Console.WriteLine($"Conditions actuelles :");
            Console.WriteLine($"  Température : {temperature:F1}°C");
            Console.WriteLine($"  Humidité : {humidity:F1}%");
            Console.WriteLine($"  Pression : {pressure:F1} hPa");
        }
    }

    /// <summary>
    /// ConcreteObserver - affiche des statistiques sur les mesures
    /// </summary>
    public class StatisticsDisplay : IWeatherObserver
    {
        public string Name { get; private set; }
        private List<double> temperatures = new List<double>();

        public StatisticsDisplay(string name)
        {
            Name = name;
        }

        public void Update(double temperature, double humidity, double pressure)
        {
            temperatures.Add(temperature);

            Console.WriteLine($"\n{Name} :");
            Console.WriteLine("Statistiques de température :");
            Console.WriteLine($"  Actuelle : {temperature:F1}°C");
            Console.WriteLine($"  Moyenne : {temperatures.Average():F1}°C");
            Console.WriteLine($"  Maximum : {temperatures.Max():F1}°C");
            Console.WriteLine($"  Minimum : {temperatures.Min():F1}°C");
        }
    }
}
