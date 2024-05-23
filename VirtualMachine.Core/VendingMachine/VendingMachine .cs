using VirtualMachine.Core.Interfaces;
using VirtualMachine.Core.VendingMachine.States;
using VirtualMachine.Data.Models;

namespace VirtualMachine.Core.VendingMachine
{
    public class VendingMachine : IVirtualMachine
    {
        private IState _state;
        public decimal CurrentBalance { get; set; }
        public string CurrentMessage { get; set; }
        public List<Product> Products { get; set; }
        public Product PurchasedProduct { get; set; }

        public VendingMachine(List<Product> products)
        {
            Products = products;
            _state = new AcceptingMoneyState(this);
        }

        public void SetState(IState state)
        {
            _state = state;
        }

        public void InsertMoney(decimal amount)
        {
            _state.InsertMoney(amount);
        }

        public void SelectProduct(int productId)
        {
            _state.SelectProduct(productId);
        }

        public void DispenseProduct()
        {
            _state.DispenseProduct();
        }

        public decimal? ReturnChange()
        {
            return _state.ReturnChange();
        }

        public static class Messages
        {
            public const string InsertedAmount = "Inserted {0:C}. Current balance: {1:C}.";
            public const string InsertAmountForProductSelection = "Please insert money before selecting a product.";
            public const string SelectProductFirst = "Please select a product first.";
            public const string ReturningChange = "Returning {0:C} in change.";
            public const string InsertAmountWhileDispensing = "Cannot insert money while dispensing a product.";
            public const string ProductAlreadySelected = "Product already selected. Dispensing in progress.";
            public const string ProductDispensed = "Dispensed {0}.";
            public const string RemainingBalance = "Remaining balance: {0:C}.";
            public const string NoRemainingBalance = "No remaining balance.";
            public const string CannotDispenseProduct = "Cannot dispense product. Either out of stock or insufficient funds.";
            public const string CannotReturnChange = "Cannot return change while dispensing a product.";
            public const string InsertAmountWhileSelecting = "Cannot insert money while dispensing a product.";
            public const string ProductNotFound = "Product not found.";
            public const string ProductOutOfStock = "Product out of stock.";
            public const string InsufficientFunds = "Insufficient funds.";
            public const string ProductSelected = "Product {0} selected.";
            public const string ReturnChangeWhileInserting = "Returning change. Please wait to insert money.";
            public const string ReturnChangeWhileSelecting = "Returning change. Please wait to select a product.";
            public const string ReturnChangeWhileDispensing = "Returning change. Please wait to dispense a product.";
        }
    }
}