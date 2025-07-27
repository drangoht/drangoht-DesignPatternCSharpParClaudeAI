using System;
using System.Collections.Generic;

namespace Patterns.Behavioral.State
{
    /// <summary>
    /// Context - Distributeur automatique
    /// </summary>
    public class VendingMachine
    {
        private IVendingMachineState _currentState;
        private readonly Dictionary<string, decimal> _products;

        public decimal Balance { get; set; }
        public string SelectedProduct { get; set; }

        public VendingMachine()
        {
            _currentState = new NoMoneyState();
            _products = new Dictionary<string, decimal>
            {
                { "A1", 1.50m },  // Eau
                { "A2", 2.00m },  // Soda
                { "B1", 1.00m },  // Chips
                { "B2", 2.50m },  // Sandwich
                { "C1", 0.80m },  // Bonbons
                { "C2", 1.20m }   // Biscuits
            };
            Balance = 0;
        }

        public void SetState(IVendingMachineState state)
        {
            _currentState = state;
        }

        public decimal GetProductPrice(string product)
        {
            return _products.TryGetValue(product, out decimal price) ? price : 0;
        }

        public void DisplayProducts()
        {
            Console.WriteLine("Produits disponibles:");
            Console.WriteLine("--------------------");
            foreach (var product in _products)
            {
                Console.WriteLine($"{product.Key}: {product.Value:C}");
            }
            Console.WriteLine();
        }

        // Actions disponibles sur le distributeur
        public void InsertMoney(decimal amount)
        {
            _currentState.InsertMoney(this, amount);
        }

        public void SelectProduct(string product)
        {
            _currentState.SelectProduct(this, product);
        }

        public void DispenseProduct()
        {
            _currentState.DispenseProduct(this);
        }

        public void CancelTransaction()
        {
            _currentState.CancelTransaction(this);
        }
    }
}
