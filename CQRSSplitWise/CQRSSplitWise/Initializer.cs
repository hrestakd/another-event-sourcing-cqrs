using System.Threading;
using System.Threading.Tasks;
using CQRSSplitWise.Rabbit;
using Microsoft.Extensions.Hosting;

namespace CQRSSplitWise
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
			_rabbitListener.Subscribe();
			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			// nothing for now
			return Task.CompletedTask;
		}
	}
}
