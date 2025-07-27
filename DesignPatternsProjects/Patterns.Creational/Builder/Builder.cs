namespace Patterns.Creational.Builder
{
    /// <summary>
    /// Interface Builder qui spécifie les méthodes pour créer les différentes parties
    /// des objets Product.
    /// </summary>
    public interface IBuilder
    {
        void Reset();
        void SetModel(string model);
        void SetEngine(string engine);
        void SetSeats(int seats);
        void SetGPS();
        void SetTripComputer();
        void SetAirConditioner();
    }

    /// <summary>
    /// Implémentation concrète du builder pour une voiture.
    /// </summary>
    public class CarBuilder : IBuilder
    {
        private Car car;

        public CarBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            car = new Car();
        }

        public void SetModel(string model)
        {
            car.Model = model;
        }

        public void SetEngine(string engine)
        {
            car.Engine = engine;
        }

        public void SetSeats(int seats)
        {
            car.Seats = seats;
        }

        public void SetGPS()
        {
            car.HasGPS = true;
        }

        public void SetTripComputer()
        {
            car.HasTripComputer = true;
        }

        public void SetAirConditioner()
        {
            car.HasAirConditioner = true;
        }

        /// <summary>
        /// Méthode pour récupérer le produit final
        /// </summary>
        public Car GetResult()
        {
            Car result = car;
            Reset(); // Prêt pour la prochaine construction
            return result;
        }
    }

    /// <summary>
    /// Builder concret pour construire le manuel d'une voiture.
    /// </summary>
    public class CarManualBuilder : IBuilder
    {
        private CarManual manual;

        public CarManualBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            manual = new CarManual();
        }

        public void SetModel(string model)
        {
            manual.Model = model;
        }

        public void SetEngine(string engine)
        {
            manual.EngineInstructions = $"Guide de démarrage pour le moteur {engine}";
        }

        public void SetSeats(int seats)
        {
            manual.SeatsInstructions = $"Comment ajuster {seats} sièges";
        }

        public void SetGPS()
        {
            manual.GPSInstructions = "Comment utiliser le GPS intégré";
        }

        public void SetTripComputer()
        {
            manual.TripComputerInstructions = "Comment configurer l'ordinateur de bord";
        }

        public void SetAirConditioner()
        {
            manual.AirConditionerInstructions = "Comment régler la climatisation";
        }

        /// <summary>
        /// Méthode pour récupérer le produit final
        /// </summary>
        public CarManual GetResult()
        {
            CarManual result = manual;
            Reset(); // Prêt pour la prochaine construction
            return result;
        }
    }
}


