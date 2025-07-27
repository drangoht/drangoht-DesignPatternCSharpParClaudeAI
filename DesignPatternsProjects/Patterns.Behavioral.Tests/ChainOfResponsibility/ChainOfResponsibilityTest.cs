using System;
using System.Threading;
using Xunit;

namespace Patterns.Behavioral.ChainOfResponsibility.Tests
{
    public class ChainOfResponsibilityTest
    {
        [Fact]
        public void TestChainOfResponsibilityPattern()
        {
            // Créer le logger
            var logger = new Logger();

            Console.WriteLine("=== Test de la chaîne de responsabilité ===\n");

            // Test 1: Message Debug
            Console.WriteLine("1. Test avec un message Debug:");
            logger.Debug("Application démarrée");
            Thread.Sleep(100); // Pause pour la lisibilité

            // Test 2: Message Info
            Console.WriteLine("\n2. Test avec un message Info:");
            logger.Info("Utilisateur connecté: admin");
            Thread.Sleep(100);

            // Test 3: Message Warning
            Console.WriteLine("\n3. Test avec un message Warning:");
            logger.Warning("Tentative de connexion échouée");
            Thread.Sleep(100);

            // Test 4: Message Error
            Console.WriteLine("\n4. Test avec un message Error:");
            logger.Error("Exception non gérée dans le module de paiement");
            Thread.Sleep(100);

            // Test 5: Test avec une chaîne personnalisée
            Console.WriteLine("\n5. Test avec une chaîne de responsabilité personnalisée:");
            
            var consoleHandler = new ConsoleLogHandler(LogLevel.Warning);
            var fileHandler = new FileLogHandler(LogLevel.Error, "errors.log");
            var emailHandler = new EmailLogHandler(LogLevel.Error, "security@example.com");

            // Configurer une chaîne différente
            consoleHandler.SetNext(emailHandler);
            emailHandler.SetNext(fileHandler);

            var customLogger = new LogHandler[] { consoleHandler, emailHandler, fileHandler };

            // Tester avec différents niveaux
            var messages = new[]
            {
                new LogMessage(LogLevel.Debug, "Message de débogage ignoré"),
                new LogMessage(LogLevel.Warning, "Attention : ressources faibles"),
                new LogMessage(LogLevel.Error, "Erreur critique : base de données inaccessible")
            };

            foreach (var message in messages)
            {
                Console.WriteLine($"\nTest avec {message.Level}:");
                customLogger[0].Handle(message);
                Thread.Sleep(100);
            }

            // Test 6: Vérification qu'un handler peut exister seul
            Console.WriteLine("\n6. Test avec un seul handler:");
            var singleHandler = new ConsoleLogHandler(LogLevel.Info);
            singleHandler.Handle(new LogMessage(LogLevel.Info, "Message avec un seul handler"));
            Thread.Sleep(100);

            // Test 7: Vérification du filtrage par niveau
            Console.WriteLine("\n7. Test du filtrage par niveau:");
            var highLevelHandler = new ConsoleLogHandler(LogLevel.Error);
            highLevelHandler.Handle(new LogMessage(LogLevel.Warning, "Ce message d'avertissement devrait être ignoré"));
            highLevelHandler.Handle(new LogMessage(LogLevel.Error, "Cette erreur devrait être affichée"));
            Thread.Sleep(100);
        }
    }
}
