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
		public GroupCreatedEventHandler _groupCreatedHandler;

		public HandlerInitializer(
			EventStoreClient client,
			UserCreatedEventHandler userCreatedHandler,
			GroupCreatedEventHandler groupCreatedHandler)
		{
			_client = client;
			_userCreatedHandler = userCreatedHandler;
			_groupCreatedHandler = groupCreatedHandler;
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

			await _client.SubscribeToStreamAsync<GroupCreatedEvent, EventMetadataBase>(EventStreams.Groups.ToString(),
				async (eventDefinition, ctoken) =>
				{
					await _groupCreatedHandler.HandleGroupCreatedEvent(eventDefinition.EventData);

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
