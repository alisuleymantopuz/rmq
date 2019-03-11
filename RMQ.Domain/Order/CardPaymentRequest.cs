namespace RMQ.Domain.Order
{
    public class CardPaymentRequest
    {
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string ExpiryDate { get; set; }
        public decimal Amount { get; set; } 
    }
}
