using System.Text;
using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;

namespace CQRSSplitWise.Rabbit
{
	public class RabbitMQPublisher
	{
		private readonly ObjectPool<IModel> _channelPool;
        
		public RabbitMQPublisher(ObjectPool<IModel> channelPool)
		{
			_channelPool = channelPool;
		}

		public void Publish()
		{
			// get channel from the object pool
			var channel = _channelPool.Get();
			
			try
			{
				var queueName = channel.DeclareTransactionQueue();
				string message = "Hello World!";
				var body = Encoding.UTF8.GetBytes(message);

				channel.BasicPublish(
					exchange: "",
					routingKey: queueName,
					basicProperties: null,
					body: body);
			}
			finally
			{
				// return object to the pool
				_channelPool.Return(channel);
			}
        }
	}
}
