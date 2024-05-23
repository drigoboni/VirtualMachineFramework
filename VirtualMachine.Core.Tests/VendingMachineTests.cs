using VirtualMachine.Core.VendingMachine.States;
using VirtualMachine.Data.Models;
using VM = VirtualMachine.Core.VendingMachine.VendingMachine;

namespace VirtualMachine.Core.Tests
{
    [TestFixture]
    public class VendingMachineTests
    {
        private VM _vendingMachine;
        private List<Product> _products;

        [SetUp]
        public void Setup()
        {
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

            _vendingMachine = new VM(_products);
        }

        [Test]
        public void InsertMoney_ShouldUpdateCurrentBalance()
        {
            // Act
            _vendingMachine.InsertMoney(1.50m);

            // Assert
            Assert.AreEqual(1.50m, _vendingMachine.CurrentBalance);
        }

        [Test]
        public void DispenseProductAfterSelection_ShouldSetPurchasedProduct_WhenProductIsAvailableAndBalanceIsSufficient()
        {
            // Arrange
            _vendingMachine.InsertMoney(2.00m);
            _vendingMachine.SetState(new ProductSelectionState(_vendingMachine));
            _vendingMachine.SelectProduct(12);

            // Act
            _vendingMachine.DispenseProduct();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(_vendingMachine.PurchasedProduct);
                Assert.AreEqual(12, _vendingMachine.PurchasedProduct.Id);
                Assert.AreEqual("Virtual Sunglasses", _vendingMachine.PurchasedProduct.Name);
            });
        }

        [Test]
        public void DispenseProduct_ShouldUpdateProductQuantity_WhenProductIsPurchased()
        {
            // Arrange
            _vendingMachine.InsertMoney(2.00m);
            _vendingMachine.SetState(new ProductSelectionState(_vendingMachine));
            _vendingMachine.SelectProduct(12);

            // Act
            _vendingMachine.DispenseProduct();

            // Assert
            var purchasedProduct = _vendingMachine.Products.Find(p => p.Id == 12);
            Assert.AreEqual(9, purchasedProduct.Quantity);
        }

        [Test]
        public void ReturnChange_ShouldReturnCorrectChange_WhenRequested()
        {
            // Arrange
            _vendingMachine.InsertMoney(2.90m);
            _vendingMachine.SetState(new ProductSelectionState(_vendingMachine));
            _vendingMachine.SelectProduct(5);
            _vendingMachine.DispenseProduct();

            // Act
            var change = _vendingMachine.ReturnChange();

            // Assert
            Assert.AreEqual(1.00m, change);
        }
    }
}