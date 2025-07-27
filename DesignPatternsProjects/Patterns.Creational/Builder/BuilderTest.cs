using System;

namespace Patterns.Creational.Builder
{
    /// <summary>
    /// Test du pattern Builder
    /// </summary>
    public class BuilderTest : Patterns.Creational.IPatternTest
    {
        public void Run()
        {
            Console.WriteLine("Démonstration du pattern Builder");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Le pattern Builder sépare la construction d'un objet complexe de sa représentation, permettant la création d'objets avec différentes configurations.");
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
            var director = new Director();
            var carBuilder = new CarBuilder();
            var manualBuilder = new CarManualBuilder();

            Console.WriteLine("1. Construction d'une voiture de base :");
            director.BuildBasicCar(carBuilder);
            var basicCar = carBuilder.GetResult();
            Console.WriteLine(basicCar);

            director.BuildBasicCar(manualBuilder);
            var basicManual = manualBuilder.GetResult();
            Console.WriteLine("\nManuel de la voiture de base :");
            Console.WriteLine(basicManual);

            Console.WriteLine("\n2. Construction d'une voiture de sport :");
            director.BuildSportsCar(carBuilder);
            var sportsCar = carBuilder.GetResult();
            Console.WriteLine(sportsCar);

            director.BuildSportsCar(manualBuilder);
            var sportsManual = manualBuilder.GetResult();
            Console.WriteLine("\nManuel de la voiture de sport :");
            Console.WriteLine(sportsManual);

            Console.WriteLine("\n3. Construction d'un SUV :");
            director.BuildSUV(carBuilder);
            var suv = carBuilder.GetResult();
            Console.WriteLine(suv);

            director.BuildSUV(manualBuilder);
            var suvManual = manualBuilder.GetResult();
            Console.WriteLine("\nManuel du SUV :");
            Console.WriteLine(suvManual);
        }

        public string GetName()
        {
            return "Builder";
        }

        public string GetDescription()
        {
            return "Le pattern Builder sépare la construction d'un objet complexe de sa représentation, permettant la création d'objets avec différentes configurations.";
        }
    }
}

