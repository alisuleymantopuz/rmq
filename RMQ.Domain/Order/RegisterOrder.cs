namespace RMQ.Domain.Order
{
    public class RegisterOrder
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string ExpiryDate { get; set; }
        public decimal Amount { get; set; } 
    }
}
