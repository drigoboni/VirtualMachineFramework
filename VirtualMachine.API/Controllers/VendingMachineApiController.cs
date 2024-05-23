using Microsoft.AspNetCore.Mvc;
using VirtualMachine.API.Models;
using VirtualMachine.Data.Repositories;
using VirtualMachine.Core.VendingMachine;
using VirtualMachine.Core.VendingMachine.States;

namespace VirtualMachine.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendingMachineApiController : ControllerBase
    {
        private readonly IVendingMachineRepository _repository;

        public VendingMachineApiController(IVendingMachineRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("products")]
        public ActionResult<IEnumerable<ProductDto>> GetProducts()
        {
            var products = _repository.GetAllProducts();
            var productDtos = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Quantity = p.Quantity
            });
            return Ok(productDtos);
        }

        [HttpPost("purchase")]
        public ActionResult Purchase([FromBody] PurchaseDto purchase)
        {
            var vendingMachine = new VendingMachine(_repository.GetAllProducts().ToList());
            vendingMachine.InsertMoney(purchase.Amount);
            vendingMachine.SetState(new ProductSelectionState(vendingMachine));
            vendingMachine.SelectProduct(purchase.ProductId);
            vendingMachine.DispenseProduct();
            var change = vendingMachine.ReturnChange();
            var purchasedProduct = vendingMachine.PurchasedProduct;

            // Update product quantity in the database
            var product = _repository.GetProductById(purchase.ProductId);

            if (product != null)
                _repository.UpdateProduct(product);

            return Ok(new { Message = "Purchase successful", Change = change, PurchasedProduct = purchasedProduct });
        }
    }
}
