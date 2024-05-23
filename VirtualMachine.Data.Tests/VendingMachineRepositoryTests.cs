using Microsoft.EntityFrameworkCore;
using Moq;
using VirtualMachine.Data.DataContext;
using VirtualMachine.Data.Models;
using VirtualMachine.Data.Repositories;

namespace VirtualMachine.Data.Tests
{
    [TestFixture]
    public class VendingMachineRepositoryTests
    {
        private Mock<VendingMachineDbContext> _contextMock;
        private IVendingMachineRepository _repository;
        private List<Product> _products;

        [SetUp]
        public void Setup()
        {
            _contextMock = new Mock<VendingMachineDbContext>();
            _repository = new VendingMachineRepository(_contextMock.Object);

            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Virtual Pet Cat", Price = 9.90m, Quantity = 10 },
                new Product { Id = 2, Name = "Virtual Pet Dog", Price = 9.90m, Quantity = 10 },
                new Product { Id = 3, Name = "Virtual Garden Kit", Price = 2.90m, Quantity = 10 },
                new Product { Id = 4, Name = "Virtual Music Album", Price = 6.90m, Quantity = 10 },
                new Product { Id = 5, Name = "Virtual Coffee", Price = 1.90m, Quantity = 10 },
                new Product { Id = 6, Name = "Virtual Energy Drink", Price = 3.90m, Quantity = 10 },
                new Product { Id = 7, Name = "Virtual Book", Price = 5.90m, Quantity = 10 },
                new Product { Id = 8, Name = "Virtual Plant", Price = 2.90m, Quantity = 10 },
                new Product { Id = 9, Name = "Virtual Adventure Pass", Price = 3.90m, Quantity = 10 },
                new Product { Id = 10, Name = "Virtual Art Piece", Price = 7.90m, Quantity = 10 },
                new Product { Id = 11, Name = "Virtual Game Token", Price = 1.90m, Quantity = 10 },
                new Product { Id = 12, Name = "Virtual Sunglasses", Price = 1.90m, Quantity = 10 }
            };
        }

        [Test]
        public void GetAllProducts_ReturnsAllProducts()
        {
            // Arrange
            _contextMock.Setup(c => c.Products).Returns(DbSetMock(_products).Object);

            // Act
            var result = _repository.GetAllProducts();

            // Assert
            Assert.AreEqual(_products.Count, result.Count());
        }

        [Test]
        public void GetProductById_ReturnsProduct_WhenProductExists()
        {
            // Arrange
            var productId = 1;
            var product = _products.FirstOrDefault(p => p.Id == productId);
            _contextMock.Setup(c => c.Products).Returns(DbSetMock(_products).Object);
            _contextMock.Setup(c => c.Products.Find(productId)).Returns(product);

            // Act
            var result = _repository.GetProductById(productId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(productId, result.Id);
        }

        [Test]
        public void GetProductById_ReturnsNull_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = 15;
            _contextMock.Setup(c => c.Products).Returns(DbSetMock(_products).Object);
            _contextMock.Setup(c => c.Products.Find(productId)).Returns((Product)null);

            // Act
            var result = _repository.GetProductById(productId);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void UpdateProduct_UpdatesProduct()
        {
            // Arrange
            var productToUpdate = _products.First();
            productToUpdate.Quantity -= 1;
            _contextMock.Setup(c => c.Products).Returns(DbSetMock(_products).Object);
            _contextMock.Setup(c => c.SaveChanges()).Returns(1);

            // Act
            _repository.UpdateProduct(productToUpdate);

            // Assert
            _contextMock.Verify(c => c.Products.Update(productToUpdate), Times.Once);
            _contextMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        private Mock<DbSet<T>> DbSetMock<T>(List<T> list) where T : class
        {
            var queryable = list.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            return dbSetMock;
        }
    }
}