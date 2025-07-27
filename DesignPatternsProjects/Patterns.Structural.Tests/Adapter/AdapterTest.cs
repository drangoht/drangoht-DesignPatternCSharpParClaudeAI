using System;
using Xunit;

namespace Patterns.Structural.Adapter.Tests
{
    public class AdapterTest
    {
        [Fact]
        public void TestAdapterPattern()
        {
            // Créer le système legacy et l'adapter
            var legacySystem = new LegacyPaymentSystem();
            var adapter = new PaymentSystemAdapter(legacySystem);
            var modernClient = new ModernPaymentClient(adapter);

            // Vérifier le solde initial (100.00€)
            Assert.Equal(100.00m, modernClient.CheckBalance("EUR"));

            // Test de paiement valide
            Assert.True(modernClient.MakePayment(50.00m, "EUR"));
            Assert.Equal(50.00m, modernClient.CheckBalance("EUR"));

            // Test de paiement avec montant négatif
            Assert.False(modernClient.MakePayment(-10.00m, "EUR"));
            Assert.Equal(50.00m, modernClient.CheckBalance("EUR"));

            // Test de paiement avec montant trop élevé
            Assert.False(modernClient.MakePayment(60.00m, "EUR"));
            Assert.Equal(50.00m, modernClient.CheckBalance("EUR"));

            // Test avec devise non supportée
            Assert.Throws<ArgumentException>(() => modernClient.MakePayment(10.00m, "USD"));
            Assert.Throws<ArgumentException>(() => modernClient.CheckBalance("USD"));

            // Test avec montants précis pour vérifier la conversion
            Assert.True(modernClient.MakePayment(10.50m, "EUR")); // 1050 cents
            Assert.Equal(39.50m, modernClient.CheckBalance("EUR"));
        }
    }
}
