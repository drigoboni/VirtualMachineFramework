using VirtualMachine.Data.Models;
using static VirtualMachine.Core.VendingMachine.VendingMachine;

namespace VirtualMachine.Core.VendingMachine.States
{
    public class DispensingProductState : IState
    {
        private readonly VendingMachine _vendingMachine;
        private readonly Product _selectedProduct;

        public DispensingProductState(VendingMachine vendingMachine, Product selectedProduct)
        {
            _vendingMachine = vendingMachine;
            _selectedProduct = selectedProduct;
        }

        public void InsertMoney(decimal amount)
        {
            _vendingMachine.CurrentMessage = Messages.InsertAmountWhileDispensing;
        }

        public void SelectProduct(int productId)
        {
            _vendingMachine.CurrentMessage = Messages.ProductAlreadySelected;
        }

        public void DispenseProduct()
        {
            if (_selectedProduct.Quantity > 0 && _vendingMachine.CurrentBalance >= _selectedProduct.Price)
            {
                _vendingMachine.CurrentBalance -= _selectedProduct.Price;
                _selectedProduct.Quantity--;
                _vendingMachine.PurchasedProduct = _selectedProduct;
                _vendingMachine.CurrentMessage = string.Format(Messages.ProductDispensed, _selectedProduct.Name);

                if (_vendingMachine.CurrentBalance > 0)
                {
                    _vendingMachine.CurrentMessage = string.Format(Messages.RemainingBalance, _vendingMachine.CurrentBalance);
                    _vendingMachine.SetState(new AcceptingMoneyState(_vendingMachine));
                }
                else
                {
                    _vendingMachine.CurrentMessage = Messages.NoRemainingBalance;
                    _vendingMachine.SetState(new ReturningChangeState(_vendingMachine));
                }
            }
            else
            {
                _vendingMachine.CurrentMessage = Messages.CannotDispenseProduct;
                _vendingMachine.SetState(new AcceptingMoneyState(_vendingMachine));
            }
        }

        public decimal? ReturnChange()
        {
            _vendingMachine.CurrentMessage = Messages.CannotReturnChange;
            return null;
        }
    }

}
