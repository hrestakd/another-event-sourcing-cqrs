using System.Threading;
using System.Threading.Tasks;
using CQRSSplitWise.Client.Query.Rabbit;
using Microsoft.Extensions.Hosting;

namespace CQRSSplitWise.Client.Query
{
	public class Initializer : IHostedService
	{
		private readonly RabbitMQListener _rabbitListener;

		public Initializer(RabbitMQListener rabbitListener)
		{
			_rabbitListener = rabbitListener;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			_rabbitListener.SubscribeTransactionConsumer();
			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			// nothing for now
			return Task.CompletedTask;
		}
	}
}
