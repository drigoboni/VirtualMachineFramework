namespace VirtualMachine.Web.Models
{
    public class VendingMachineViewModel
    {
        public List<ProductViewModel> Products { get; set; }
        public decimal CurrentBalance { get; set; }
        public List<ProductViewModel> PurchasedProducts { get; set; }
    }
}
