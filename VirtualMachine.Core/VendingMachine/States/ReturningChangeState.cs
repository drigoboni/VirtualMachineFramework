using static VirtualMachine.Core.VendingMachine.VendingMachine;

namespace VirtualMachine.Core.VendingMachine.States
{
    public class ReturningChangeState : IState
    {
        private readonly VendingMachine _vendingMachine;

        public ReturningChangeState(VendingMachine vendingMachine)
        {
            _vendingMachine = vendingMachine;
        }

        public void InsertMoney(decimal amount)
        {
            _vendingMachine.CurrentMessage = Messages.ReturnChangeWhileInserting;
        }

        public void SelectProduct(int productId)
        {
            _vendingMachine.CurrentMessage = Messages.ReturnChangeWhileSelecting;
        }

        public void DispenseProduct()
        {
            _vendingMachine.CurrentMessage = Messages.ReturnChangeWhileDispensing;
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
