using CQRSSplitWise.Client.Query.Services;
using CQRSSplitWise.Extensions.Rabbit;
using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CQRSSplitWise.Client.Query.Rabbit
{
	public class RabbitMQListener
	{
		private readonly ProcessTransactionEventHandler _transactionEventHandler;
		private readonly ObjectPool<IModel> _channelPool;


		public RabbitMQListener(ProcessTransactionEventHandler transactionEventHandler, ObjectPool<IModel> channelPool)
		{
            _transactionEventHandler = transactionEventHandler;
			_channelPool = channelPool;
		}

		public void SubscribeTransactionConsumer()
		{
			_channelPool.UseTransactionChannel((channel, queueName) =>
			{
				var consumer = new EventingBasicConsumer(channel);
				consumer.Received += async (model, eventArgs) =>
				{
					var transactionEvent = eventArgs.Body.ToArray();
					await _transactionEventHandler.ProcessEvent(transactionEvent);
				};

				channel.BasicConsume(queue: queueName,
										autoAck: true,
										consumer: consumer);
			});
        }
	}
}
