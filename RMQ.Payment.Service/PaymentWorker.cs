using RMQ.Domain.Order;
using System;
using System.Drawing;
using Console = Colorful.Console;

namespace RMQ.Payment.Service
{
    public class PaymentWorker
    {
        public CardPaymentResponse Execute(CardPaymentRequest request)
        {
            CardPaymentResponse responseMessage = new CardPaymentResponse
            {
                AuthCode = Guid.NewGuid().ToString().Substring(0, 4).ToUpper()
            };

            Console.WriteLine("Worker activated to process response =>" + responseMessage.AuthCode, Color.Azure);

            return responseMessage;
        }
    }
}
