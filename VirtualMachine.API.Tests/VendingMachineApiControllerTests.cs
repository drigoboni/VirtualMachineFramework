using Microsoft.AspNetCore.Mvc;
using Moq;
using VirtualMachine.API.Controllers;
using VirtualMachine.API.Models;
using VirtualMachine.Data.Models;
using VirtualMachine.Data.Repositories;

namespace VirtualMachine.API.Tests
{
    [TestFixture]
    public class VendingMachineApiControllerTests
    {
        private Mock<IVendingMachineRepository> _mockRepository;
        private VendingMachineApiController _controller;
        private List<Product> _products;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IVendingMachineRepository>();
            _controller = new VendingMachineApiController(_mockRepository.Object);

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
        public void GetProducts_ReturnsListOfProductDtos()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAllProducts()).Returns(_products);

            // Act
            var result = _controller.GetProducts();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            var returnedProducts = okResult.Value as IEnumerable<ProductDto>;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(12, returnedProducts.Count());
                Assert.AreEqual(1, returnedProducts.First().Id);
                Assert.AreEqual("Virtual Pet Cat", returnedProducts.First().Name);
            });
        }

        [Test]
        public void Purchase_ReturnsSuccessMessageAndChange()
        {
            // Arrange
            var purchaseDto = new PurchaseDto { ProductId = 1, Amount = 10.90m };

            _mockRepository.Setup(repo => repo.GetAllProducts()).Returns(_products);
            _mockRepository.Setup(repo => repo.GetProductById(1)).Returns(_products.First());

            // Act
            var result = _controller.Purchase(purchaseDto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            dynamic response = okResult.Value;

            var message = response.GetType().GetProperty("Message").GetValue(response);
            var change = response.GetType().GetProperty("Change").GetValue(response);
            var purchasedProduct = response.GetType().GetProperty("PurchasedProduct").GetValue(response);

            Assert.Multiple(() =>
            {
                Assert.AreEqual("Purchase successful", message);
                Assert.AreEqual(1.0m, change);
                Assert.IsNotNull(purchasedProduct);
            });
        }

        [Test]
        public void Purchase_ProductNotFound_ReturnsNotFound()
        {
            // Arrange
            var products = new List<Product>();
            var purchaseDto = new PurchaseDto { ProductId = 1, Amount = 2.0m };

            _mockRepository.Setup(repo => repo.GetAllProducts()).Returns(products);
            _mockRepository.Setup(repo => repo.GetProductById(1)).Returns((Product)null);

            // Act
            var result = _controller.Purchase(purchaseDto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            dynamic response = okResult.Value;

            var message = response.GetType().GetProperty("Message").GetValue(response);
            var change = response.GetType().GetProperty("Change").GetValue(response);
            var purchasedProduct = response.GetType().GetProperty("PurchasedProduct").GetValue(response);

            Assert.Multiple(() =>
            {
                Assert.AreEqual("Purchase successful", message);
                Assert.AreEqual(2.0m, change);
                Assert.IsNull(purchasedProduct);
            });
        }
    }
}