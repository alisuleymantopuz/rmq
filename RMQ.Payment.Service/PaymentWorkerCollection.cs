using System.Collections.Concurrent;

namespace RMQ.Payment.Service
{
    public class PaymentWorkerCollection
    {
        public BlockingCollection<PaymentWorker> Populate()
        {
            var workers = new BlockingCollection<PaymentWorker>();

            for (int i = 0; i < 10; i++)
            {
                workers.Add(new PaymentWorker());
            }

            return workers;
        }
    }
}
