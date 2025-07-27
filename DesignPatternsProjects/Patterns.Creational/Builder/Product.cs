using System;
using System.Collections.Generic;
using System.Text;

namespace Patterns.Creational.Builder
{
    /// <summary>
    /// Produit complexe qui est construit par le Builder.
    /// Une voiture possède de nombreuses caractéristiques optionnelles.
    /// </summary>
    public class Car
    {
        // Une voiture peut avoir ou non un GPS, trip computer, etc.
        public string Model { get; set; }
        public string Engine { get; set; }
        public int Seats { get; set; }
        public bool HasGPS { get; set; }
        public bool HasTripComputer { get; set; }
        public bool HasAirConditioner { get; set; }
        
        public Car() { }
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Voiture modèle: {Model}");
            sb.AppendLine($"Moteur: {Engine}");
            sb.AppendLine($"Nombre de sièges: {Seats}");
            sb.AppendLine($"GPS: {(HasGPS ? "Oui" : "Non")}");
            sb.AppendLine($"Ordinateur de bord: {(HasTripComputer ? "Oui" : "Non")}");
            sb.AppendLine($"Climatisation: {(HasAirConditioner ? "Oui" : "Non")}");
            return sb.ToString();
        }
    }

    /// <summary>
    /// Un autre type de produit - pour montrer comment le même Builder peut construire différents produits.
    /// </summary>
    public class CarManual
    {
        public string Model { get; set; }
        public string EngineInstructions { get; set; }
        public string SeatsInstructions { get; set; }
        public string GPSInstructions { get; set; }
        public string TripComputerInstructions { get; set; }
        public string AirConditionerInstructions { get; set; }
        
        public CarManual() { }
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"MANUEL D'UTILISATION - {Model}");
            sb.AppendLine($"Instructions du moteur: {EngineInstructions}");
            sb.AppendLine($"Instructions des sièges: {SeatsInstructions}");
            
            if (!string.IsNullOrEmpty(GPSInstructions))
                sb.AppendLine($"Instructions GPS: {GPSInstructions}");
                
            if (!string.IsNullOrEmpty(TripComputerInstructions))
                sb.AppendLine($"Instructions ordinateur de bord: {TripComputerInstructions}");
                
            if (!string.IsNullOrEmpty(AirConditionerInstructions))
                sb.AppendLine($"Instructions climatisation: {AirConditionerInstructions}");
                
            return sb.ToString();
        }
    }
}


