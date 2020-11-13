using CQRSSplitWise.Services.Read;
using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CQRSSplitWise.Rabbit
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

		public void Subscribe()
		{
			// get channel from the object pool
			var channel = _channelPool.Get();

			try
			{
				var queueName = channel.DeclareTransactionQueue();

				var consumer = new EventingBasicConsumer(channel);
				consumer.Received += async (model, ea) =>
				{
					var eventData = ea.Body.ToArray();
					await _transactionEventHandler.ProcessEvent(eventData);
				};

				channel.BasicConsume(queue: queueName,
										autoAck: true,
										consumer: consumer);
			}
			finally
			{
				// return object to the pool
				_channelPool.Return(channel);
			}
        }
	}
}
