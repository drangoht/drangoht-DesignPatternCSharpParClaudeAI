using System;

namespace Patterns.Structural.Adapter
{
    /// <summary>
    /// Target - INewPayment
    /// Interface moderne de paiement
    /// </summary>
    public interface INewPayment
    {
        bool ProcessPayment(decimal amount, string currency);
        decimal GetBalance(string currency);
    }

    /// <summary>
    /// Adaptee - LegacyPaymentSystem
    /// Ancien système de paiement qui utilise des montants en centimes et uniquement en euros
    /// </summary>
    public class LegacyPaymentSystem
    {
        private long _balanceCents;

        public LegacyPaymentSystem()
        {
            _balanceCents = 10000; // 100.00€ de solde initial
        }

        public bool ProcessPaymentInCents(long amountCents)
        {
            if (amountCents <= 0)
            {
                Console.WriteLine("Le montant doit être positif");
                return false;
            }

            if (amountCents > _balanceCents)
            {
                Console.WriteLine("Solde insuffisant");
                return false;
            }

            _balanceCents -= amountCents;
            Console.WriteLine($"Paiement de {amountCents} centimes traité. Nouveau solde: {_balanceCents} centimes");
            return true;
        }

        public long GetBalanceInCents()
        {
            return _balanceCents;
        }
    }

    /// <summary>
    /// Adapter - PaymentSystemAdapter
    /// Adapte le système legacy pour qu'il fonctionne avec la nouvelle interface
    /// </summary>
    public class PaymentSystemAdapter : INewPayment
    {
        private readonly LegacyPaymentSystem _legacySystem;

        public PaymentSystemAdapter(LegacyPaymentSystem legacySystem)
        {
            _legacySystem = legacySystem;
        }

        public bool ProcessPayment(decimal amount, string currency)
        {
            if (currency != "EUR")
            {
                throw new ArgumentException("Le système legacy ne supporte que l'euro (EUR)");
            }

            long amountCents = (long)(amount * 100);
            return _legacySystem.ProcessPaymentInCents(amountCents);
        }

        public decimal GetBalance(string currency)
        {
            if (currency != "EUR")
            {
                throw new ArgumentException("Le système legacy ne supporte que l'euro (EUR)");
            }

            long balanceCents = _legacySystem.GetBalanceInCents();
            return balanceCents / 100.0m;
        }
    }

    /// <summary>
    /// Client moderne utilisant la nouvelle interface
    /// </summary>
    public class ModernPaymentClient
    {
        private readonly INewPayment _paymentSystem;

        public ModernPaymentClient(INewPayment paymentSystem)
        {
            _paymentSystem = paymentSystem;
        }

        public bool MakePayment(decimal amount, string currency)
        {
            Console.WriteLine($"Tentative de paiement de {amount} {currency}");
            return _paymentSystem.ProcessPayment(amount, currency);
        }

        public decimal CheckBalance(string currency)
        {
            return _paymentSystem.GetBalance(currency);
        }
    }
}
