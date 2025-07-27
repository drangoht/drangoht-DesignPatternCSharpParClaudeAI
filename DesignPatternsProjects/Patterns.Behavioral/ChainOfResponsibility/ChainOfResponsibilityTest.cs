using System;

namespace Patterns.Behavioral.ChainOfResponsibility
{
    /// <summary>
    /// Test du pattern ChainOfResponsibility
    /// </summary>
    public class ChainOfResponsibilityTest : Patterns.Behavioral.IPatternTest
    {
        public void Run()
        {
            Console.WriteLine("Démonstration du pattern ChainOfResponsibility");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Le pattern Chain of Responsibility évite le couplage de l'expéditeur d'une requête à son destinataire en donnant à plusieurs objets la possibilité de traiter la requête.");
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
            // Création de la chaîne de responsabilité
            Console.WriteLine("Configuration de la chaîne de support:");
            Console.WriteLine("--------------------------------------");
            
            // Création des handlers
            SupportHandler level1 = new Level1Support();
            SupportHandler level2 = new Level2Support();
            SupportHandler level3 = new Level3Support();
            SupportHandler manager = new EmergencySupport();
            
            // Configuration de la chaîne
            level1.SetNext(level2).SetNext(level3).SetNext(manager);
            
            // Affichage de la chaîne
            Console.WriteLine("Chaîne de responsabilité: ");
            level1.DisplayChain();
            Console.WriteLine();
            
            // Création et traitement des demandes de support
            Console.WriteLine("Traitement des demandes de support:");
            Console.WriteLine("----------------------------------");
            
            // Demande de niveau Basic
            var request1 = new SupportTicket(
                "Problème de connexion à l'application",
                SupportLevel.Basic
            );
            Console.WriteLine($"\nDemande reçue: {request1.Description}");
            level1.ProcessTicket(request1);
            
            // Demande de niveau Standard
            var request2 = new SupportTicket(
                "Problème avec l'import de données",
                SupportLevel.Standard
            );
            Console.WriteLine($"\nDemande reçue: {request2.Description}");
            level1.ProcessTicket(request2);
            
            // Demande de niveau Critical
            var request3 = new SupportTicket(
                "Erreur dans le module de facturation",
                SupportLevel.Critical
            );
            Console.WriteLine($"\nDemande reçue: {request3.Description}");
            level1.ProcessTicket(request3);
            
            // Demande de niveau Emergency
            var request4 = new SupportTicket(
                "Serveur production inaccessible",
                SupportLevel.Emergency
            );
            Console.WriteLine($"\nDemande reçue: {request4.Description}");
            level1.ProcessTicket(request4);
            
            Console.WriteLine("\nLe pattern Chain of Responsibility permet de:");
            Console.WriteLine("- Découpler l'expéditeur et le destinataire d'une requête");
            Console.WriteLine("- Permettre à plusieurs objets de traiter une requête");
            Console.WriteLine("- Définir une chaîne d'objets de traitement dynamiquement");
        }

        public string GetName()
        {
            return "ChainOfResponsibility";
        }

        public string GetDescription()
        {
            return "Le pattern Chain of Responsibility évite le couplage de l'expéditeur d'une requête à son destinataire en donnant à plusieurs objets la possibilité de traiter la requête.";
        }
    }
}

