using CQRSSplitWise.Client.Query.DAL.Models;
using CQRSSplitWise.Client.Query.DAL.Repositories;
using CQRSSplitWise.Client.Query.Users.DTO;
using CQRSSplitWise.Client.Query.Users.EventHandlers;
using CQRSSplitWise.Client.Query.Users.Filters;
using CQRSSplitWise.DataContracts.Enums;
using CQRSSplitWise.DataContracts.Events;
using EventStore.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;

namespace CQRSSplitWise.Client.Query.Users.Services
{
	public class UserService
	{
		private readonly IRepository<UserData> _repository;
		private readonly EventStoreClient _eventStoreClient;
		private readonly UserCreatedEventHandler _userCreatedHandler;

		public UserService(
			IRepository<UserData> repository,
			EventStoreClient eventStoreClient,
			UserCreatedEventHandler userCreatedEventHandler)
		{
			_repository = repository;
			_eventStoreClient = eventStoreClient;
			_userCreatedHandler = userCreatedEventHandler;
		}

		public async Task<IEnumerable<UserDTO>> GetUsers(UserFilter userFilter)
		{
			var expressions = GenerateExpressions(userFilter);

			IEnumerable<UserData> users;

			if (expressions == null || expressions.Count == 0)
			{
				users = await _repository.GetData(null);
			}
			else
			{
				users = await _repository.GetData(expressions);
			}

			var data = new List<UserDTO>();
			foreach (var user in users)
			{
				data.Add(new UserDTO
				{
					UserID = user.UserID,
					FirstName = user.Name,
					LastName = user.LastName
				});
			}

			return data;
		}

		public async Task RebuildUsers()
		{
			await _repository.DropCollection();

			var fullStream = _eventStoreClient
				.ReadStreamAsync(
					Direction.Forwards,
					EventStreams.Users.ToString(),
					StreamPosition.Start);

			var events = await fullStream.ToListAsync();

			foreach (var @event in events)
			{
				var userCreated = JsonSerializer.Deserialize<UserCreatedEvent>(@event.OriginalEvent.Data.ToArray());
				await _userCreatedHandler.HandleUserCreatedEvent(userCreated);
			}
		}

		private List<Expression<Func<UserData, bool>>> GenerateExpressions(UserFilter filter)
		{
			var expressions = new List<Expression<Func<UserData, bool>>>
			{
				x => true
			};

			if (filter == null)
			{
				return expressions;
			}

			if (filter.UserID > 0)
			{
				expressions.Add(x => x.UserID == filter.UserID);
			}

			if (!string.IsNullOrWhiteSpace(filter.FirstName))
			{
				expressions.Add(x => x.Name.Contains(filter.FirstName, StringComparison.InvariantCultureIgnoreCase));
			}

			if (!string.IsNullOrWhiteSpace(filter.LastName))
			{
				expressions.Add(x => x.LastName.Contains(filter.LastName, StringComparison.InvariantCultureIgnoreCase));
			}

			return expressions;
		}
	}
}
