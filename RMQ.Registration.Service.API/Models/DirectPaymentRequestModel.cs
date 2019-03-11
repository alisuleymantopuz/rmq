namespace RMQ.Registration.Service.API.Models
{
    public class DirectPaymentRequestModel
    {
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string ExpiryDate { get; set; }
        public decimal Amount { get; set; }
    }
}
