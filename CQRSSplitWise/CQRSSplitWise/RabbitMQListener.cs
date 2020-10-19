using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRSSplitWise.Services.Read;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CQRSSplitWise
{
	public class RabbitMQListener
	{
        private const string TRANSACTION_QUEUE_NAME = "TransactionEvents";
		private readonly ProcessTransactionEventHandler _transactionEventHandler;

		public RabbitMQListener(ProcessTransactionEventHandler transactionEventHandler)
		{
            _transactionEventHandler = transactionEventHandler;

            Subscribe();
		}

		public void Subscribe()
		{
            var factory = new ConnectionFactory { HostName = "rabbit" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: TRANSACTION_QUEUE_NAME,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += async (model, ea) =>
                {
                    var eventData = ea.Body.ToArray();
                    await _transactionEventHandler.ProcessEvent(eventData);
                };

                channel.BasicConsume(queue: TRANSACTION_QUEUE_NAME,
                                     autoAck: true,
                                     consumer: consumer);
            }
        }
	}
}
