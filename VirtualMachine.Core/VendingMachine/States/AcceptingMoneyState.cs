using static VirtualMachine.Core.VendingMachine.VendingMachine;

namespace VirtualMachine.Core.VendingMachine.States
{
    public class AcceptingMoneyState : IState
    {
        private VendingMachine _vendingMachine;

        public AcceptingMoneyState(VendingMachine vendingMachine)
        {
            _vendingMachine = vendingMachine;
        }

        public void InsertMoney(decimal amount)
        {
            _vendingMachine.CurrentBalance += amount;
            _vendingMachine.CurrentMessage = string.Format(Messages.InsertedAmount, amount, _vendingMachine.CurrentBalance);
        }

        public void SelectProduct(int productId)
        {
            _vendingMachine.CurrentMessage = Messages.InsertAmountForProductSelection;
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
