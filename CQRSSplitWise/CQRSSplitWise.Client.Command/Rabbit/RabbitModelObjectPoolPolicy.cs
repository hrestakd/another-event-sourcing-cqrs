using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;

namespace CQRSSplitWise.Rabbit
{
	/// <summary>
	/// While a Channel can be used by multiple threads, 
	/// it's important to ensure that only one thread executes a command at once. 
	/// Concurrent execution of commands will likely cause an UnexpectedFrameError to be thrown.
	/// </summary>
	public class RabbitModelObjectPoolPolicy : IPooledObjectPolicy<IModel>
	{
		private readonly IConnection _connection;

		public RabbitModelObjectPoolPolicy()
		{
			_connection = GetConnection();
		}

		private IConnection GetConnection()
		{
			var factory = new ConnectionFactory { HostName = "rabbit" };

			return factory.CreateConnection();
		}

		public IModel Create()
		{
			return _connection.CreateModel();
		}

		public bool Return(IModel channel)
		{
			if (channel.IsOpen)
			{
				return true;
			}
			else
			{
				channel?.Dispose();
				return false;
			}
		}
	}
}
