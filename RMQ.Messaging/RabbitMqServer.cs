namespace RMQ.Messaging
{
    public static class RabbitMqServer
    {
        public const string ConStr = "RMQConStr";

        public static class Finance
        {
            public const string FinanceQueue = "finance.service";
        }

        public static class Registration
        {
            public const string OrderQueue = "registerorder.service";
        }

        public static class Notification
        {
            public const string NotificationQueue = "notification.service";
        }
    }
}
