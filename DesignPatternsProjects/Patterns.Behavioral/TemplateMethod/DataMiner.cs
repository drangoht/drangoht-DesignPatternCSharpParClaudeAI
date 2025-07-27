using System;
using System.Collections.Generic;

namespace Patterns.Behavioral.TemplateMethod
{
    /// <summary>
    /// Abstract Class - DataMiner
    /// Définit le template pour l'analyse de données
    /// </summary>
    public abstract class DataMiner
    {
        // Template Method
        public void Mine(string path)
        {
            var data = LoadData(path);
            var processedData = ProcessData(data);
            var analysis = AnalyzeData(processedData);
            SendReport(analysis);

            if (ShouldCache())
            {
                Console.WriteLine("Mise en cache des résultats...");
                CacheResults(analysis);
            }

            Cleanup();
        }

        // Primitive Operations (doivent être implémentées par les sous-classes)
        protected abstract string[] LoadData(string path);
        protected abstract object ProcessData(string[] data);
        protected abstract Dictionary<string, double> AnalyzeData(object data);

        // Hook Methods (peuvent être surchargées par les sous-classes)
        protected virtual void SendReport(Dictionary<string, double> analysis)
        {
            Console.WriteLine("\nRésultats de l'analyse:");
            foreach (var result in analysis)
            {
                Console.WriteLine($"{result.Key}: {result.Value}");
            }
        }

        protected virtual bool ShouldCache()
        {
            return false;
        }

        protected virtual void CacheResults(Dictionary<string, double> analysis)
        {
            // Par défaut ne fait rien
        }

        protected virtual void Cleanup()
        {
            Console.WriteLine("Nettoyage des ressources...");
        }
    }

    /// <summary>
    /// Concrete Class - CSV Data Miner
    /// </summary>
    public class CsvDataMiner : DataMiner
    {
        protected override string[] LoadData(string path)
        {
            Console.WriteLine($"Chargement du fichier CSV: {path}");
            // Simulation de lecture de fichier CSV
            return new[]
            {
                "Date,Value",
                "2025-01-01,100",
                "2025-01-02,150",
                "2025-01-03,120",
                "2025-01-04,180"
            };
        }

        protected override object ProcessData(string[] data)
        {
            Console.WriteLine("Traitement des données CSV...");
            var values = new List<double>();
            
            // Ignorer l'en-tête
            for (int i = 1; i < data.Length; i++)
            {
                var parts = data[i].Split(',');
                if (double.TryParse(parts[1], out double value))
                {
                    values.Add(value);
                }
            }
            
            return values;
        }

        protected override Dictionary<string, double> AnalyzeData(object data)
        {
            Console.WriteLine("Analyse des données CSV...");
            var values = (List<double>)data;
            var results = new Dictionary<string, double>();

            results["Count"] = values.Count;
            results["Sum"] = values.Sum();
            results["Average"] = values.Average();
            results["Min"] = values.Min();
            results["Max"] = values.Max();

            return results;
        }

        protected override bool ShouldCache()
        {
            return true;
        }

        protected override void CacheResults(Dictionary<string, double> analysis)
        {
            Console.WriteLine("Mise en cache des résultats CSV...");
            // Simulation de mise en cache
        }
    }

    /// <summary>
    /// Concrete Class - Log Data Miner
    /// </summary>
    public class LogDataMiner : DataMiner
    {
        protected override string[] LoadData(string path)
        {
            Console.WriteLine($"Chargement du fichier log: {path}");
            // Simulation de lecture de fichier log
            return new[]
            {
                "[INFO] 2025-01-01 10:00:00 Response time: 100ms",
                "[INFO] 2025-01-01 10:01:00 Response time: 150ms",
                "[ERROR] 2025-01-01 10:02:00 Timeout",
                "[INFO] 2025-01-01 10:03:00 Response time: 120ms"
            };
        }

        protected override object ProcessData(string[] data)
        {
            Console.WriteLine("Traitement des données de log...");
            var stats = new Dictionary<string, int>
            {
                ["INFO"] = 0,
                ["ERROR"] = 0,
                ["WARN"] = 0
            };

            foreach (var line in data)
            {
                if (line.Contains("[INFO]")) stats["INFO"]++;
                if (line.Contains("[ERROR]")) stats["ERROR"]++;
                if (line.Contains("[WARN]")) stats["WARN"]++;
            }

            return stats;
        }

        protected override Dictionary<string, double> AnalyzeData(object data)
        {
            Console.WriteLine("Analyse des données de log...");
            var stats = (Dictionary<string, int>)data;
            var total = stats.Values.Sum();
            var results = new Dictionary<string, double>();

            results["Total Entries"] = total;
            foreach (var kvp in stats)
            {
                results[$"{kvp.Key} %"] = total > 0 ? (kvp.Value * 100.0 / total) : 0;
            }

            return results;
        }

        protected override void SendReport(Dictionary<string, double> analysis)
        {
            Console.WriteLine("\nRAPPORT D'ANALYSE DE LOG");
            Console.WriteLine("------------------------");
            base.SendReport(analysis);
        }
    }

    /// <summary>
    /// Concrete Class - Database Data Miner
    /// </summary>
    public class DatabaseDataMiner : DataMiner
    {
        private bool _isConnected = false;

        protected override string[] LoadData(string path)
        {
            Console.WriteLine($"Connexion à la base de données: {path}");
            _isConnected = true;
            // Simulation de requête SQL
            return new[]
            {
                "SELECT * FROM metrics",
                "CPU: 75%",
                "Memory: 60%",
                "Disk: 45%"
            };
        }

        protected override object ProcessData(string[] data)
        {
            Console.WriteLine("Traitement des données de la base...");
            var metrics = new Dictionary<string, double>();
            
            foreach (var line in data.Skip(1)) // Skip SQL query
            {
                var parts = line.Split(':');
                if (parts.Length == 2 && parts[1].Trim().EndsWith("%"))
                {
                    var value = double.Parse(parts[1].Trim().TrimEnd('%'));
                    metrics[parts[0].Trim()] = value;
                }
            }

            return metrics;
        }

        protected override Dictionary<string, double> AnalyzeData(object data)
        {
            Console.WriteLine("Analyse des métriques de la base...");
            var metrics = (Dictionary<string, double>)data;
            var results = new Dictionary<string, double>();

            // Calculer la charge moyenne
            results["Average Load"] = metrics.Values.Average();
            
            // Identifier les métriques critiques (> 70%)
            results["Critical Metrics"] = metrics.Count(m => m.Value > 70);

            // Copier les métriques individuelles
            foreach (var metric in metrics)
            {
                results[metric.Key] = metric.Value;
            }

            return results;
        }

        protected override void Cleanup()
        {
            if (_isConnected)
            {
                Console.WriteLine("Fermeture de la connexion à la base de données...");
                _isConnected = false;
            }
            base.Cleanup();
        }
    }
}
