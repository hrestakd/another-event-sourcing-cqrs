using CQRSSplitWise.DataContracts.Events;
using CQRSSplitWise.Extensions.Rabbit;
using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;

namespace CQRSSplitWise.Client.Command.Rabbit
{
	public class RabbitMQPublisher
	{
		private readonly ObjectPool<IModel> _channelPool;
        
		public RabbitMQPublisher(ObjectPool<IModel> channelPool)
		{
			_channelPool = channelPool;
		}

		public void PublishTransactionEvent(TransactionEventData transactionEventData)
		{
			_channelPool.UseTransactionChannel((channel, queueName) =>
			{
				var transactionEvent = new TransactionEvent(transactionEventData, null);
				var body = Extensions.ByteArrayExtensions.ObjectToByteArray(transactionEvent);
				channel.BasicPublish(
					exchange: "",
					routingKey: queueName,
					basicProperties: null,
					body: body);
			});
        }
	}
}
