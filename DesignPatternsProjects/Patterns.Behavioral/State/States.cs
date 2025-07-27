namespace Patterns.Behavioral.State
{
    /// <summary>
    /// State - Interface définissant les actions possibles pour chaque état
    /// </summary>
    public interface IVendingMachineState
    {
        void InsertMoney(VendingMachine machine, decimal amount);
        void SelectProduct(VendingMachine machine, string product);
        void DispenseProduct(VendingMachine machine);
        void CancelTransaction(VendingMachine machine);
    }

    /// <summary>
    /// Concrete State - État Initial (pas d'argent inséré)
    /// </summary>
    public class NoMoneyState : IVendingMachineState
    {
        public void InsertMoney(VendingMachine machine, decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Le montant doit être positif");
                return;
            }
            machine.Balance += amount;
            Console.WriteLine($"Argent inséré: {amount:C}. Solde total: {machine.Balance:C}");
            machine.SetState(new HasMoneyState());
        }

        public void SelectProduct(VendingMachine machine, string product)
        {
            Console.WriteLine("Veuillez d'abord insérer de l'argent");
        }

        public void DispenseProduct(VendingMachine machine)
        {
            Console.WriteLine("Veuillez d'abord insérer de l'argent");
        }

        public void CancelTransaction(VendingMachine machine)
        {
            Console.WriteLine("Aucune transaction en cours");
        }
    }

    /// <summary>
    /// Concrete State - État avec argent inséré
    /// </summary>
    public class HasMoneyState : IVendingMachineState
    {
        public void InsertMoney(VendingMachine machine, decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Le montant doit être positif");
                return;
            }
            machine.Balance += amount;
            Console.WriteLine($"Argent inséré: {amount:C}. Solde total: {machine.Balance:C}");
        }

        public void SelectProduct(VendingMachine machine, string product)
        {
            decimal price = machine.GetProductPrice(product);
            if (price == 0)
            {
                Console.WriteLine("Produit non disponible");
                return;
            }

            if (machine.Balance >= price)
            {
                machine.SelectedProduct = product;
                machine.SetState(new ProductSelectedState());
                Console.WriteLine($"Produit sélectionné: {product} - Prix: {price:C}");
            }
            else
            {
                Console.WriteLine($"Solde insuffisant. Prix: {price:C}, Solde: {machine.Balance:C}");
            }
        }

        public void DispenseProduct(VendingMachine machine)
        {
            Console.WriteLine("Veuillez d'abord sélectionner un produit");
        }

        public void CancelTransaction(VendingMachine machine)
        {
            Console.WriteLine($"Transaction annulée. Remboursement: {machine.Balance:C}");
            machine.Balance = 0;
            machine.SetState(new NoMoneyState());
        }
    }

    /// <summary>
    /// Concrete State - État avec produit sélectionné
    /// </summary>
    public class ProductSelectedState : IVendingMachineState
    {
        public void InsertMoney(VendingMachine machine, decimal amount)
        {
            Console.WriteLine("Produit déjà sélectionné. Veuillez finaliser ou annuler la transaction");
        }

        public void SelectProduct(VendingMachine machine, string product)
        {
            Console.WriteLine("Produit déjà sélectionné. Veuillez finaliser ou annuler la transaction");
        }

        public void DispenseProduct(VendingMachine machine)
        {
            decimal price = machine.GetProductPrice(machine.SelectedProduct);
            machine.Balance -= price;
            Console.WriteLine($"Distribution du produit: {machine.SelectedProduct}");
            
            if (machine.Balance > 0)
            {
                Console.WriteLine($"N'oubliez pas votre monnaie: {machine.Balance:C}");
                machine.Balance = 0;
            }
            
            machine.SelectedProduct = null;
            machine.SetState(new NoMoneyState());
        }

        public void CancelTransaction(VendingMachine machine)
        {
            Console.WriteLine($"Transaction annulée. Remboursement: {machine.Balance:C}");
            machine.Balance = 0;
            machine.SelectedProduct = null;
            machine.SetState(new NoMoneyState());
        }
    }
}
