using static VirtualMachine.Core.VendingMachine.VendingMachine;

namespace VirtualMachine.Core.VendingMachine.States
{
    public class ProductSelectionState : IState
    {
        private readonly VendingMachine _vendingMachine;

        public ProductSelectionState(VendingMachine vendingMachine)
        {
            _vendingMachine = vendingMachine;
        }

        public void InsertMoney(decimal amount)
        {
            _vendingMachine.CurrentMessage = Messages.InsertAmountWhileSelecting;
        }

        public void SelectProduct(int productId)
        {
            var product = _vendingMachine.Products.FirstOrDefault(p => p.Id == productId);

            if (product == null)
            {
                _vendingMachine.CurrentMessage = Messages.ProductNotFound;
                return;
            }

            if (product.Quantity <= 0)
            {
                _vendingMachine.CurrentMessage = Messages.ProductOutOfStock;
                return;
            }

            if (_vendingMachine.CurrentBalance < product.Price)
            {
                _vendingMachine.CurrentMessage = Messages.InsufficientFunds;
                return;
            }

            _vendingMachine.CurrentMessage = string.Format(Messages.ProductSelected, product.Name);
            _vendingMachine.SetState(new DispensingProductState(_vendingMachine, product));
        }

        public void DispenseProduct()
        {
            _vendingMachine.CurrentMessage = Messages.SelectProductFirst;
        }

        public decimal? ReturnChange()
        {
            var change = _vendingMachine.CurrentBalance;
            _vendingMachine.CurrentMessage = string.Format(Messages.ReturningChange, change);
            _vendingMachine.CurrentBalance = 0;
            _vendingMachine.SetState(new AcceptingMoneyState(_vendingMachine));
            return change;
        }
    }

}
