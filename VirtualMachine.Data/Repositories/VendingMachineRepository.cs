using VirtualMachine.Data.DataContext;
using VirtualMachine.Data.Models;

namespace VirtualMachine.Data.Repositories
{
    public class VendingMachineRepository : IVendingMachineRepository
    {
        private readonly VendingMachineDbContext _context;

        public VendingMachineRepository(VendingMachineDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        public Product GetProductByName(string name)
        {
            return _context.Products.FirstOrDefault(p => p.Name == name);
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
    }
}
