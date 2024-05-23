using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VirtualMachine.Web.Extensions;
using VirtualMachine.Web.Models;

namespace VirtualMachine.Web.Controllers
{
    public class VendingMachineController : Controller
    {
        private const string CurrentBalanceSessionKey = "CurrentBalance";
        private const string PurchasedProductsSessionKey = "PurchasedProducts";

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public decimal CurrentBalance
        {
            get => HttpContext.Session.GetDecimal(CurrentBalanceSessionKey);
            set => HttpContext.Session.SetDecimal(CurrentBalanceSessionKey, value);
        }
        public List<ProductViewModel> PurchasedProducts
        {
            get => HttpContext.Session.Get<List<ProductViewModel>>(PurchasedProductsSessionKey) ?? new List<ProductViewModel>();
            set => HttpContext.Session.Set<List<ProductViewModel>>(PurchasedProductsSessionKey, value);
        }

        public VendingMachineController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("VendingMachineAPI");
            var response = await client.GetAsync(_configuration["ApiEndpoints:Products"]);
            response.EnsureSuccessStatusCode();

            var products = await response.Content.ReadFromJsonAsync<IEnumerable<ProductViewModel>>();

            var viewModel = new VendingMachineViewModel
            {
                Products = products?.ToList() ?? new List<ProductViewModel>(),
                CurrentBalance = CurrentBalance,
                PurchasedProducts = PurchasedProducts ?? new List<ProductViewModel>(),
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddMoney(decimal amount)
        {
            CurrentBalance += amount;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Withdraw()
        {
            CurrentBalance = 0;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Purchase(int productId)
        {
            var purchaseDto = new PurchaseDto
            {
                ProductId = productId,
                Amount = CurrentBalance
            };

            var client = _httpClientFactory.CreateClient("VendingMachineAPI");
            var response = await client.PostAsJsonAsync(_configuration["ApiEndpoints:Purchase"], purchaseDto);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<PurchaseResultDto>();
            CurrentBalance = result?.Change ?? 0;
            AddPurchasedProduct(result?.PurchasedProduct);

            return RedirectToAction(nameof(Index));
        }

        public void AddPurchasedProduct(ProductDto? purchasedProduct)
        {
            if (purchasedProduct == null) return;

            var purchasedProducts = PurchasedProducts;

            purchasedProducts = purchasedProducts ?? new List<ProductViewModel>();
            var existingProduct = purchasedProducts.FirstOrDefault(p => p.Id == purchasedProduct.Id);

            if (existingProduct != null)
            {
                existingProduct.Quantity++;
            }
            else
            {
                purchasedProducts.Add(new ProductViewModel
                {
                    Id = purchasedProduct.Id,
                    Name = purchasedProduct.Name,
                    Price = purchasedProduct.Price,
                    Quantity = 1
                });
            }
            PurchasedProducts = purchasedProducts;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
