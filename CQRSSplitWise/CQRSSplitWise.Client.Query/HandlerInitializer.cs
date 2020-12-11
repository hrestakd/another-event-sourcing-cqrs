using System;
using System.Threading;
using System.Threading.Tasks;
using CQRSSplitWise.Client.Query.EventHandlers;
using CQRSSplitWise.Client.Query.Rabbit;
using CQRSSplitWise.DataContracts.Enums;
using CQRSSplitWise.DataContracts.Events;
using EventStore.Client;
using EventStoreDB.Extensions;
using Microsoft.Extensions.Hosting;

namespace CQRSSplitWise.Client.Query
{
	public class HandlerInitializer : IHostedService
	{
		public EventStoreClient _client { get; }
		public UserCreatedEventHandler _userCreatedHandler;

		public HandlerInitializer(
			EventStoreClient client,
			UserCreatedEventHandler userCreatedHandler)
		{
			_client = client;
			_userCreatedHandler = userCreatedHandler;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			await _client.SubscribeToStreamAsync<UserCreatedEvent, EventMetadataBase>(EventStreams.Users.ToString(),
				async (eventDefinition, ctoken) =>
				{
					await _userCreatedHandler.HandleUserCreatedEvent(eventDefinition.EventData);

					return;
				},
				null);

			return;
		}

		public async Task StopAsync(CancellationToken cancellationToken)
		{
			// nothing for now
			return;
		}
	}
}
