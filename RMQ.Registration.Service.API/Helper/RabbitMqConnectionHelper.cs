using EasyNetQ;
using Microsoft.Extensions.Configuration;
using RMQ.Messaging;

namespace RMQ.Registration.Service.API.Helper
{
    public class RabbitMqConnectionHelper
    {
        public IConfiguration Configuration { get; set; }

        public RabbitMqConnectionHelper(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IBus Get()
        {
            return RabbitHutch.CreateBus(Configuration[RabbitMqServer.ConStr]);
        }
    }
}
