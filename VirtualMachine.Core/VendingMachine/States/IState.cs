namespace VirtualMachine.Core.VendingMachine.States
{
    public interface IState
    {
        void InsertMoney(decimal amount);
        void SelectProduct(int productId);
        void DispenseProduct();
        decimal? ReturnChange();
    }
}
