namespace BookStore.Web.ViewModels
{
    public class BillingDetailViewModel
    {
        public string Address { get; set; }
        public decimal GrandTotal { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string ExpireNumber { get; set; }
        public string CVV { get; set; }
    }
}
