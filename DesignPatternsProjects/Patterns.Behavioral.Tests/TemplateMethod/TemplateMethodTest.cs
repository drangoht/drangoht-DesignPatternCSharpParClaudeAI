using System;
using Xunit;

namespace Patterns.Behavioral.TemplateMethod.Tests
{
    public class TemplateMethodTest
    {
        [Fact]
        public void TestTemplateMethodPattern()
        {
            Console.WriteLine("=== Test de l'analyseur CSV ===");
            var csvMiner = new CsvDataMiner();
            csvMiner.Mine("data.csv");

            Console.WriteLine("\n=== Test de l'analyseur de logs ===");
            var logMiner = new LogDataMiner();
            logMiner.Mine("app.log");

            Console.WriteLine("\n=== Test de l'analyseur de base de données ===");
            var dbMiner = new DatabaseDataMiner();
            dbMiner.Mine("jdbc:postgresql://localhost:5432/metrics");

            // Test spécifique pour l'analyseur CSV
            var csvMiner2 = new CsvDataMiner();
            Assert.True(csvMiner2.ShouldCache()); // Vérifie que la mise en cache est activée

            // Test spécifique pour l'analyseur de logs
            var logMiner2 = new LogDataMiner();
            Assert.False(logMiner2.ShouldCache()); // Vérifie que la mise en cache est désactivée

            // Test de l'ordre d'exécution
            Console.WriteLine("\n=== Test de l'ordre d'exécution ===");
            var executionOrder = new List<string>();
            
            var testMiner = new TestDataMiner(executionOrder);
            testMiner.Mine("test.data");

            // Vérifie l'ordre d'exécution des étapes
            Assert.Equal("LoadData", executionOrder[0]);
            Assert.Equal("ProcessData", executionOrder[1]);
            Assert.Equal("AnalyzeData", executionOrder[2]);
            Assert.Equal("SendReport", executionOrder[3]);
            Assert.Equal("Cleanup", executionOrder[4]);
        }

        /// <summary>
        /// Classe de test pour vérifier l'ordre d'exécution
        /// </summary>
        private class TestDataMiner : DataMiner
        {
            private readonly List<string> _executionOrder;

            public TestDataMiner(List<string> executionOrder)
            {
                _executionOrder = executionOrder;
            }

            protected override string[] LoadData(string path)
            {
                _executionOrder.Add("LoadData");
                return new[] { "Test Data" };
            }

            protected override object ProcessData(string[] data)
            {
                _executionOrder.Add("ProcessData");
                return new Dictionary<string, double>();
            }

            protected override Dictionary<string, double> AnalyzeData(object data)
            {
                _executionOrder.Add("AnalyzeData");
                return new Dictionary<string, double>();
            }

            protected override void SendReport(Dictionary<string, double> analysis)
            {
                _executionOrder.Add("SendReport");
            }

            protected override void Cleanup()
            {
                _executionOrder.Add("Cleanup");
            }

            protected override bool ShouldCache()
            {
                return false;
            }
        }
    }
}
