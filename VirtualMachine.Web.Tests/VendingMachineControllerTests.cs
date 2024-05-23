using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using System.Net;
using VirtualMachine.Web.Controllers;
using VirtualMachine.Web.Models;

namespace VirtualMachine.Web.Tests
{
    [TestFixture]
    public class VendingMachineControllerTests
    {
        private Mock<IHttpClientFactory> _httpClientFactoryMock;
        private Mock<IConfiguration> _configurationMock;
        private Mock<HttpContext> _httpContextMock;
        private List<ProductViewModel> _products;

        [SetUp]
        public void Setup()
        {
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _configurationMock = new Mock<IConfiguration>();

            _httpContextMock = new Mock<HttpContext>();
            var sessionMock = new Mock<MockSession>();
            _httpContextMock.SetupGet(c => c.Session).Returns(sessionMock.Object);

            _products = new List<ProductViewModel>
            {
                new ProductViewModel { Id = 1, Name = "Virtual Pet Cat", Price = 9.90m, Quantity = 10 },
                new ProductViewModel { Id = 2, Name = "Virtual Pet Dog", Price = 9.90m, Quantity = 10 },
                new ProductViewModel { Id = 3, Name = "Virtual Garden Kit", Price = 2.90m, Quantity = 10 },
                new ProductViewModel { Id = 4, Name = "Virtual Music Album", Price = 6.90m, Quantity = 10 },
                new ProductViewModel { Id = 5, Name = "Virtual Coffee", Price = 1.90m, Quantity = 10 },
                new ProductViewModel { Id = 6, Name = "Virtual Energy Drink", Price = 3.90m, Quantity = 10 },
                new ProductViewModel { Id = 7, Name = "Virtual Book", Price = 5.90m, Quantity = 10 },
                new ProductViewModel { Id = 8, Name = "Virtual Plant", Price = 2.90m, Quantity = 10 },
                new ProductViewModel { Id = 9, Name = "Virtual Adventure Pass", Price = 3.90m, Quantity = 10 },
                new ProductViewModel { Id = 10, Name = "Virtual Art Piece", Price = 7.90m, Quantity = 10 },
                new ProductViewModel { Id = 11, Name = "Virtual Game Token", Price = 1.90m, Quantity = 10 },
                new ProductViewModel { Id = 12, Name = "Virtual Sunglasses", Price = 1.90m, Quantity = 10 }
            };
        }

        [Test]
        public async Task Index_SetUpApiRequestAndReturnView_WithViewModel()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("[]")
                })
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://localhost:5120")
            };

            _httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            _configurationMock.Setup(c => c["ApiEndpoints:Products"]).Returns("VendingMachineApi/products");

            var controller = new VendingMachineController(_httpClientFactoryMock.Object, _configurationMock.Object)
            {
                ControllerContext = new ControllerContext { HttpContext = _httpContextMock.Object }
            };

            // Act
            var result = await controller.Index() as ViewResult;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.IsInstanceOf<VendingMachineViewModel>(result.Model);
                var viewModel = result.Model as VendingMachineViewModel;
                Assert.IsNotNull(viewModel);
            });

            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get &&
                    req.RequestUri == new Uri("http://localhost:5120/VendingMachineApi/products")
                ),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Test]
        public async Task AddMoney_IncreasesCurrentBalance_RedirectsToIndex()
        {
            // Arrange
            var controller = new VendingMachineController(_httpClientFactoryMock.Object, _configurationMock.Object)
            {
                ControllerContext = new ControllerContext { HttpContext = _httpContextMock.Object }
            };
            const decimal amount = 2.0m;

            // Act
            var result = controller.AddMoney(amount) as RedirectToActionResult;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.AreEqual("Index", result.ActionName);
                Assert.AreEqual(2.0m, controller.CurrentBalance);
            });

        }

        [Test]
        public async Task Withdraw_ResetsCurrentBalance_RedirectsToIndex()
        {
            // Arrange
            var controller = new VendingMachineController(_httpClientFactoryMock.Object, _configurationMock.Object)
            {
                ControllerContext = new ControllerContext { HttpContext = _httpContextMock.Object }
            };
            controller.CurrentBalance = 10.0m;

            // Act
            var result = controller.Withdraw() as RedirectToActionResult;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.AreEqual("Index", result.ActionName);
                Assert.AreEqual(0.0m, controller.CurrentBalance);
            });
        }

        [Test]
        public async Task Purchase_AddsPurchasedProduct_RedirectsToIndex()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"Change\": 2.0, \"PurchasedProduct\": {\"Id\": 1, \"Name\": \"Virtual Pet Cat\", \"Price\": 9.90, \"Quantity\": 1}}")
                })
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://localhost:5120")
            };

            _httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            _configurationMock.Setup(c => c["ApiEndpoints:Purchase"]).Returns("VendingMachineApi/purchase");

            var controller = new VendingMachineController(_httpClientFactoryMock.Object, _configurationMock.Object)
            {
                ControllerContext = new ControllerContext { HttpContext = _httpContextMock.Object }
            };

            // Act
            var result = await controller.Purchase(1) as RedirectToActionResult;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.AreEqual("Index", result.ActionName);
                Assert.AreEqual(2.0m, controller.CurrentBalance);
                Assert.AreEqual(1, controller.PurchasedProducts.Count);
                Assert.AreEqual(1, controller.PurchasedProducts.FirstOrDefault().Id);
            });
        }

        [Test]
        public void AddPurchasedProduct_AddsProductToPurchasedProducts()
        {
            // Arrange
            var controller = new VendingMachineController(_httpClientFactoryMock.Object, _configurationMock.Object)
            {
                ControllerContext = new ControllerContext { HttpContext = _httpContextMock.Object }
            };
            controller.CurrentBalance = 0;
            controller.PurchasedProducts = new List<ProductViewModel>();
            var purchasedProduct = new ProductDto { Id = 1, Name = "Virtual Pet Cat", Price = 9.90m, Quantity = 1 };

            // Act
            controller.AddPurchasedProduct(purchasedProduct);

            // Assert
            Assert.AreEqual(1, controller.PurchasedProducts.Count);
            Assert.AreEqual(1, controller.PurchasedProducts.FirstOrDefault().Id);
        }

        public class MockSession : ISession
        {
            private readonly Dictionary<string, byte[]> _sessionStorage = new Dictionary<string, byte[]>();

            public IEnumerable<string> Keys => _sessionStorage.Keys;

            public string Id { get; } = Guid.NewGuid().ToString();

            public bool IsAvailable { get; } = true;

            public void Clear() => _sessionStorage.Clear();

            public Task CommitAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

            public Task LoadAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

            public void Remove(string key) => _sessionStorage.Remove(key);

            public void Set(string key, byte[] value) => _sessionStorage[key] = value;

            public bool TryGetValue(string key, out byte[] value) => _sessionStorage.TryGetValue(key, out value);
        }
    }
}