using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace CQRSSplitWise
{
	public class RabbitMQPublisher
	{
        private const string TRANSACTION_QUEUE_NAME = "TransactionEvents";

        public void Publish()
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

                string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: TRANSACTION_QUEUE_NAME,
                    basicProperties: null,
                    body: body);
            }
        }
	}
}
