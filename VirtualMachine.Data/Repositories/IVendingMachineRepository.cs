using VirtualMachine.Data.Models;

namespace VirtualMachine.Data.Repositories
{
    public interface IVendingMachineRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        Product GetProductByName(string name);
        void UpdateProduct(Product product);
    }
}
