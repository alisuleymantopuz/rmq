using Microsoft.AspNetCore.Mvc;
using RMQ.Domain.Order;
using RMQ.Messaging;
using RMQ.Registration.Service.API.Helper;
using RMQ.Registration.Service.API.Models;
using System;

namespace RMQ.Registration.Service.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        public RabbitMqConnectionHelper RabbitMqConnectionHelper { get; set; }

        public PaymentController(RabbitMqConnectionHelper rabbitMqConnectionHelper)
        {
            RabbitMqConnectionHelper = rabbitMqConnectionHelper;
        }

        [HttpPost]
        [Route("RegisterOrder")]
        public IActionResult RegisterOrder([FromBody]RegisterOrderModel registerOrderInfo)
        {
            try
            {
                var registerOrder = new RegisterOrder()
                {
                    Amount = registerOrderInfo.Amount,
                    CardHolderName = registerOrderInfo.CardHolderName,
                    CardNumber = registerOrderInfo.CardNumber,
                    ExpiryDate = registerOrderInfo.ExpiryDate,
                    Address = registerOrderInfo.Address,
                    Email = registerOrderInfo.Email,
                    Name = registerOrderInfo.Name
                };


                using (var bus = RabbitMqConnectionHelper.Get())
                {
                    bus.Publish(registerOrder, cfg => cfg.WithQueueName(RabbitMqServer.Registration.OrderQueue));

                    return Ok();
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        [Route("DirectPayment")]
        public IActionResult DirectPayment([FromBody]DirectPaymentRequestModel directPaymentRequestModel)
        {
            try
            {
                var cardPaymentRequest = new CardPaymentRequest()
                {
                    Amount = directPaymentRequestModel.Amount,
                    CardHolderName = directPaymentRequestModel.CardHolderName,
                    CardNumber = directPaymentRequestModel.CardNumber,
                    ExpiryDate = directPaymentRequestModel.ExpiryDate
                };
                
                using (var bus = RabbitMqConnectionHelper.Get())
                {
                    var response = bus.Request<CardPaymentRequest, CardPaymentResponse>(cardPaymentRequest);

                    return Ok(response);
                }

            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
