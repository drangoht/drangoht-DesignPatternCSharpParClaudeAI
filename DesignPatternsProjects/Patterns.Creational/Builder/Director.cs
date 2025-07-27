namespace Patterns.Creational.Builder
{
    /// <summary>
    /// Le Director est responsable de l'exécution des étapes de construction dans un ordre particulier.
    /// Il travaille avec n'importe quel builder qui suit l'interface IBuilder.
    /// </summary>
    public class Director
    {
        /// <summary>
        /// Construit une voiture simple de base
        /// </summary>
        public void BuildBasicCar(IBuilder builder)
        {
            builder.Reset();
            builder.SetModel("Modèle de base");
            builder.SetEngine("1.2L 3-cylindres");
            builder.SetSeats(4);
        }

        /// <summary>
        /// Construit une voiture sportive
        /// </summary>
        public void BuildSportsCar(IBuilder builder)
        {
            builder.Reset();
            builder.SetModel("Sport GT");
            builder.SetEngine("3.0L V6 Turbo");
            builder.SetSeats(2);
            builder.SetGPS();
            builder.SetTripComputer();
            builder.SetAirConditioner();
        }

        /// <summary>
        /// Construit un SUV familial
        /// </summary>
        public void BuildSUV(IBuilder builder)
        {
            builder.Reset();
            builder.SetModel("SUV Familial");
            builder.SetEngine("2.0L 4-cylindres");
            builder.SetSeats(7);
            builder.SetGPS();
            builder.SetAirConditioner();
        }
    }
}


