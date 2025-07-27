using System;
using System.Linq;
using Xunit;

namespace Patterns.Behavioral.Mediator.Tests
{
    public class MediatorTest
    {
        [Fact]
        public void TestMediatorPattern()
        {
            Console.WriteLine("=== Test du pattern Mediator avec une salle de chat ===\n");

            // Créer la salle de chat
            var chatRoom = new ChatRoom();

            // Créer les utilisateurs
            var alice = new User("Alice", chatRoom);
            var bob = new User("Bob", chatRoom);
            var charlie = new User("Charlie", chatRoom);
            var david = new User("David", chatRoom);

            // Test 1: Enregistrement des utilisateurs
            Console.WriteLine("1. Test d'enregistrement des utilisateurs");
            chatRoom.Register(alice);
            chatRoom.Register(bob);
            chatRoom.Register(charlie);
            chatRoom.Register(david);

            Assert.Equal(4, chatRoom.GetUsers().Count);

            // Test 2: Messages publics
            Console.WriteLine("\n2. Test des messages publics");
            alice.Send("Bonjour tout le monde!");
            bob.Send("Salut Alice!");

            // Vérifier que tous les utilisateurs ont reçu les messages
            Assert.Contains(bob.MessageHistory, m => m.Contains("Alice: Bonjour tout le monde!"));
            Assert.Contains(charlie.MessageHistory, m => m.Contains("Bob: Salut Alice!"));

            // Test 3: Messages privés
            Console.WriteLine("\n3. Test des messages privés");
            alice.SendPrivate("Hey Bob, comment vas-tu?", bob);
            bob.SendPrivate("Je vais bien, merci!", alice);

            // Vérifier que seuls Alice et Bob ont les messages privés
            Assert.Contains(alice.MessageHistory, m => m.Contains("[Privé]"));
            Assert.Contains(bob.MessageHistory, m => m.Contains("[Privé]"));
            Assert.DoesNotContain(charlie.MessageHistory, m => m.Contains("[Privé]"));

            // Test 4: Création et utilisation de groupes
            Console.WriteLine("\n4. Test des groupes de discussion");
            var groupMembers = new List<User> { alice, bob, charlie };
            alice.CreateGroup("Projet A", groupMembers);

            // Vérifier que le groupe est créé
            Assert.Single(chatRoom.GetGroups());
            Assert.Contains("Projet A", chatRoom.GetGroups());

            // Test 5: Messages de groupe
            Console.WriteLine("\n5. Test des messages de groupe");
            bob.SendToGroup("Réunion à 14h?", "Projet A");
            charlie.SendToGroup("OK pour moi", "Projet A");

            // Vérifier que les membres du groupe reçoivent les messages
            Assert.Contains(alice.MessageHistory, m => m.Contains("[Groupe: Projet A]"));
            Assert.Contains(bob.MessageHistory, m => m.Contains("[Groupe: Projet A]"));
            Assert.Contains(charlie.MessageHistory, m => m.Contains("[Groupe: Projet A]"));
            Assert.DoesNotContain(david.MessageHistory, m => m.Contains("[Groupe: Projet A]"));

            // Test 6: Tentative d'envoi à un groupe inexistant
            Console.WriteLine("\n6. Test d'envoi à un groupe inexistant");
            david.SendToGroup("Hello!", "Groupe Inconnu");
            Assert.Contains(david.MessageHistory, m => m.Contains("n'existe pas"));

            // Test 7: Tentative d'envoi à un utilisateur qui n'existe pas
            Console.WriteLine("\n7. Test d'envoi à un utilisateur inexistant");
            var eve = new User("Eve", chatRoom);
            alice.SendPrivate("Hello Eve!", eve);
            Assert.Contains(alice.MessageHistory, m => m.Contains("Utilisateur non trouvé"));

            // Test 8: Message à soi-même
            Console.WriteLine("\n8. Test d'envoi d'un message à soi-même");
            alice.SendPrivate("Note à moi-même", alice);
            Assert.Contains(alice.MessageHistory, 
                m => m.Contains("[Privé]") && m.Contains("Alice: Note à moi-même"));

            // Afficher les statistiques finales
            Console.WriteLine("\nStatistiques finales:");
            Console.WriteLine($"Nombre d'utilisateurs: {chatRoom.GetUsers().Count}");
            Console.WriteLine($"Nombre de groupes: {chatRoom.GetGroups().Count}");
            Console.WriteLine($"Messages reçus par Alice: {alice.MessageHistory.Count}");
            Console.WriteLine($"Messages reçus par Bob: {bob.MessageHistory.Count}");
            Console.WriteLine($"Messages reçus par Charlie: {charlie.MessageHistory.Count}");
            Console.WriteLine($"Messages reçus par David: {david.MessageHistory.Count}");
        }
    }
}
