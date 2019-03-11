using System;

namespace RMQ.Domain.Order
{
    public class OrderNotification
    {
        public string AuthCode { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}
