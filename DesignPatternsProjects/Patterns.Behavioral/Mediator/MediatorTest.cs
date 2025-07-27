using System;

namespace Patterns.Behavioral.Mediator
{
    /// <summary>
    /// Test du pattern Mediator
    /// </summary>
    public class MediatorTest : Patterns.Behavioral.IPatternTest
    {
        public void Run()
        {
            Console.WriteLine("Démonstration du pattern Mediator");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Le pattern Mediator définit un objet qui encapsule comment un ensemble d'objets interagissent.");
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
            // Création du médiateur
            Console.WriteLine("Création d'un salon de discussion (médiateur)...");
            IChatMediator chatRoom = new ChatRoom("Salon principal");
            
            // Création des utilisateurs (collègues)
            Console.WriteLine("\nAjout d'utilisateurs au salon...");
            ChatUser alice = new RegularUser("Alice", chatRoom);
            ChatUser bob = new RegularUser("Bob", chatRoom);
            ChatUser charlie = new ModeratorUser("Charlie", chatRoom);
            ChatUser diana = new AdminUser("Diana", chatRoom);
            
            Console.WriteLine("\nDémonstration des communications via le médiateur:");
            
            // Messages normaux (de tous à tous via le médiateur)
            alice.Send("Bonjour tout le monde !");
            bob.Send("Salut Alice et les autres !");

            // Message ciblé
            alice.SendTo("Hey Bob, comment vas-tu ?", bob);
            bob.SendTo("Je vais bien, merci Alice !", alice);

            // Message privé
            charlie.SendPrivate("Message confidentiel pour Diana", diana);
            diana.SendPrivate("Bien reçu Charlie", charlie);

            // Annonce (seuls les modérateurs et admins peuvent le faire)
            Console.WriteLine("\nTentative d'annonce par un utilisateur normal:");
            alice.Broadcast("Je fais une annonce !");

            Console.WriteLine("\nAnnonce par un modérateur:");
            charlie.Broadcast("Attention, message important du modérateur !");

            Console.WriteLine("\nAnnonce par un administrateur:");
            diana.Broadcast("Message de l'administrateur système.");
            
            Console.WriteLine("\nLe pattern Mediator permet de:");
            Console.WriteLine("- Découpler les objets en éliminant les références directes entre eux");
            Console.WriteLine("- Centraliser les communications complexes dans un seul objet médiateur");
            Console.WriteLine("- Simplifier les interactions entre de nombreux objets");
        }

        public string GetName()
        {
            return "Mediator";
        }

        public string GetDescription()
        {
            return "Le pattern Mediator définit un objet qui encapsule comment un ensemble d'objets interagissent.";
        }
    }
}

