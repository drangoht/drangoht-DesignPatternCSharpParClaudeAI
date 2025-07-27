using System;
using Xunit;

namespace Patterns.Behavioral.State.Tests
{
    public class StateTest
    {
        [Fact]
        public void TestStatePattern()
        {
            // Créer un distributeur
            var machine = new VendingMachine();

            // Afficher les produits disponibles
            machine.DisplayProducts();

            // Test État Initial (NoMoneyState)
            machine.SelectProduct("A1");  // Devrait échouer car pas d'argent
            machine.DispenseProduct();     // Devrait échouer car pas d'argent
            machine.CancelTransaction();   // Devrait indiquer qu'aucune transaction n'est en cours

            // Test insertion d'argent (passage à HasMoneyState)
            machine.InsertMoney(2.00m);
            Assert.Equal(2.00m, machine.Balance);

            // Test sélection de produit invalide
            machine.SelectProduct("X1");  // Produit non existant
            Assert.Equal(2.00m, machine.Balance);

            // Test sélection de produit trop cher
            machine.SelectProduct("B2");  // Prix: 2.50€
            Assert.Equal(2.00m, machine.Balance);

            // Test sélection de produit valide (passage à ProductSelectedState)
            machine.SelectProduct("A1");  // Prix: 1.50€
            Assert.Equal("A1", machine.SelectedProduct);

            // Test tentative d'insertion d'argent après sélection
            machine.InsertMoney(1.00m);  // Devrait être refusé

            // Test distribution du produit
            machine.DispenseProduct();
            Assert.Equal(0.50m, machine.Balance);  // Monnaie rendue
            Assert.Null(machine.SelectedProduct);

            // Test nouvelle transaction
            machine.InsertMoney(3.00m);
            machine.SelectProduct("B2");  // Prix: 2.50€
            Assert.Equal("B2", machine.SelectedProduct);

            // Test annulation de transaction
            machine.CancelTransaction();
            Assert.Equal(0m, machine.Balance);
            Assert.Null(machine.SelectedProduct);
        }
    }
}
