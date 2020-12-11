using System;
using System.Threading;
using System.Threading.Tasks;
using CQRSSplitWise.Client.Query.EventHandlers;
using CQRSSplitWise.DataContracts.Enums;
using CQRSSplitWise.DataContracts.Events;
using EventStore.Client;
using EventStoreDB.Extensions;
using Microsoft.Extensions.Hosting;

namespace CQRSSplitWise.Client.Query
{
	public class HandlerInitializer : IHostedService
	{
		private readonly EventStoreClient _eventStoreClient;
		private readonly UserCreatedEventHandler _userCreatedHandler;
		private readonly GroupCreatedEventHandler _groupCreatedHandler;
		private readonly AddedUsersToGroupEventHandler _addUsersToGroupHandler;
		private readonly CreateTransactionEventHandler _createTransactionHandler;

		public HandlerInitializer(
			EventStoreClient client,
			UserCreatedEventHandler userCreatedHandler,
			GroupCreatedEventHandler groupCreatedHandler,
			AddedUsersToGroupEventHandler addUsersToGroupHandler,
			CreateTransactionEventHandler createTransactionHandler)
		{
			_eventStoreClient = client;
			_userCreatedHandler = userCreatedHandler;
			_groupCreatedHandler = groupCreatedHandler;
			_addUsersToGroupHandler = addUsersToGroupHandler;
			_createTransactionHandler = createTransactionHandler;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			await _eventStoreClient.SubscribeToStreamAsync<UserCreatedEvent, EventMetadataBase>(EventStreams.Users.ToString(),
				async (eventDefinition, ctoken) =>
				{
					await _userCreatedHandler.HandleUserCreatedEvent(eventDefinition.EventData);

					return;
				},
				null);

			await _eventStoreClient.SubscribeToStreamAsync<GroupCreatedEvent, EventMetadataBase>(EventStreams.Groups.ToString(),
				async (eventDefinition, ctoken) =>
				{
					await _groupCreatedHandler.HandleGroupCreatedEvent(eventDefinition.EventData);

					return;
				},
				null);

			await _eventStoreClient.SubscribeToStreamAsync<UsersAddedToGroupEvent, EventMetadataBase>(EventStreams.Groups.ToString(),
				async (eventDefinition, ctoken) =>
				{
					await _addUsersToGroupHandler.HandleAddedUserToGroupEvent(eventDefinition.EventData);

					return;
				},
				null);

			await _eventStoreClient.SubscribeToStreamAsync<CreateTransactionEvent, EventMetadataBase>(EventStreams.Transactions.ToString(),
				async (eventDefinition, ctoken) =>
				{
					await _createTransactionHandler.HandleCreateTransactionEvent(eventDefinition.EventData);

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
