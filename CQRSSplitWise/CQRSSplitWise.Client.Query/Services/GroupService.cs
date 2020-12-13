using CQRSSplitWise.Client.Query.DAL.Models;
using CQRSSplitWise.Client.Query.DAL.Repositories;
using CQRSSplitWise.Client.Query.DTO;
using CQRSSplitWise.Client.Query.EventHandlers;
using CQRSSplitWise.Client.Query.Filters;
using CQRSSplitWise.DataContracts.Enums;
using CQRSSplitWise.DataContracts.Events;
using EventStore.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;

namespace CQRSSplitWise.Client.Query.Services
{
	public class GroupService
	{
		private readonly IRepository<GroupData> _repository;
		private readonly EventStoreClient _eventStoreClient;
		private readonly GroupCreatedEventHandler _groupCreatedHandler;
		private readonly AddedUsersToGroupEventHandler _addUsersToGroupHandler;

		public GroupService(
			IRepository<GroupData> repository,
			EventStoreClient eventStoreClient,
			GroupCreatedEventHandler groupCreatedEventHandler,
			AddedUsersToGroupEventHandler addedUsersToGroupEventHandler)
		{
			_repository = repository;
			_eventStoreClient = eventStoreClient;
			_groupCreatedHandler = groupCreatedEventHandler;
			_addUsersToGroupHandler = addedUsersToGroupEventHandler;
		}

		public async Task<IEnumerable<GroupDTO>> GetGroups(GroupFilter userFilter)
		{
			var expressions = GenerateExpressions(userFilter);

			IEnumerable<GroupData> groups;

			if (expressions == null || expressions.Count == 0)
			{
				groups = await _repository.GetData(null);
			}
			else
			{
				groups = await _repository.GetData(expressions);
			}

			var data = new List<GroupDTO>();
			foreach (var group in groups)
			{
				data.Add(new GroupDTO
				{
					GroupID = group.GroupID,
					GroupName = group.GroupName,
					UsersInGroup = group.UsersInGroup
						.Select(x => new UserDTO
						{
							UserID = x.UserID,
							FirstName = x.Name,
							LastName = x.LastName
						})
				});
			}

			return data;
		}

		public async Task RebuildGroups()
		{
			await _repository.DropCollection();

			var fullStream = _eventStoreClient
				.ReadStreamAsync(
					Direction.Forwards,
					EventStreams.Groups.ToString(),
					StreamPosition.Start);

			var events = await fullStream.ToListAsync();

			foreach (var @event in events)
			{
				var eventType = @event.OriginalEvent.EventType;
				if (eventType == EventTypes.GroupCreated.ToString())
				{
					var groupCreated = JsonSerializer.Deserialize<GroupCreatedEvent>(@event.OriginalEvent.Data.ToArray());
					await _groupCreatedHandler.HandleGroupCreatedEvent(groupCreated);
				}
				else if (eventType == EventTypes.UsersAddedToGroup.ToString())
				{
					var usersAdded = JsonSerializer.Deserialize<UsersAddedToGroupEvent>(@event.OriginalEvent.Data.ToArray());
					await _addUsersToGroupHandler.HandleAddedUserToGroupEvent(usersAdded);
				}
			}
		}

		private List<Expression<Func<GroupData, bool>>> GenerateExpressions(GroupFilter filter)
		{
			var expressions = new List<Expression<Func<GroupData, bool>>>
			{
				x => true
			};

			if (filter == null)
			{
				return expressions;
			}

			if (filter.GroupID > 0)
			{
				expressions.Add(x => x.GroupID == filter.GroupID);
			}

			if (!string.IsNullOrWhiteSpace(filter.GroupName))
			{
				expressions.Add(x => x.GroupName.Contains(filter.GroupName, StringComparison.InvariantCultureIgnoreCase));
			}

			if (filter.GroupsWithUserID > 0)
			{
				expressions.Add(x => x.UsersInGroup
					.Any(y => y.UserID == filter.GroupsWithUserID));
			}

			return expressions;
		}
	}
}
