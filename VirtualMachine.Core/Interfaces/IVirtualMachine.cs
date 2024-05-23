namespace VirtualMachine.Core.Interfaces
{
    public interface IVirtualMachine
    {
        void InsertMoney(decimal amount);
        void SelectProduct(int productId);
        void DispenseProduct();
        decimal? ReturnChange();
    }
}
