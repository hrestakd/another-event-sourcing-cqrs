using AutoMapper;
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
	public class UserBalanceService
	{
		private readonly IRepository<UserBalance> _userBalanceRepository;
		private readonly EventStoreClient _eventStoreClient;
		private readonly UpdateBalanceEventHandler _balanceEventHandler;

		public UserBalanceService(
			IRepository<UserBalance> userBalanceRepository,
			EventStoreClient eventStoreClient,
			UpdateBalanceEventHandler updateBalanceEventHandler)
		{
			_userBalanceRepository = userBalanceRepository;
			_eventStoreClient = eventStoreClient;
			_balanceEventHandler = updateBalanceEventHandler;
		}

		public async Task<UserBalanceDTO> GetUserBalance(int userID)
		{
			var expressions = new List<Expression<Func<UserBalance, bool>>>
			{
				x => x.SourceUserData.UserID == userID
			};

			var usersData = await _userBalanceRepository.GetData(expressions);

			if (!usersData.Any())
			{
				return new UserBalanceDTO();
			}

			var targetUserData = usersData
				.Select(x => x.SourceUserData)
				.FirstOrDefault();

			var userBalanceDTO = new UserBalanceDTO
			{
				UserData = new UserDTO
				{
					FirstName = targetUserData.Name,
					LastName = targetUserData.LastName,
					UserID = targetUserData.UserID
				},
				TotalBalance = usersData
					.Sum(x => x.TotalBalance),
				Balances = usersData
					.Select(x => new BalanceForUser
					{
						Balance = x.TotalBalance,
						UserData = new UserDTO
						{
							FirstName = x.DestUserData.Name,
							LastName = x.DestUserData.LastName,
							UserID = x.DestUserData.UserID
						}
					})
			};

			return userBalanceDTO;
		}

		public async Task RebuildBalance()
		{
			await _userBalanceRepository.DropCollection();

			var fullStream = _eventStoreClient
				.ReadStreamAsync(
					Direction.Forwards,
					EventStreams.Transactions.ToString(),
					StreamPosition.Start);

			var events = await fullStream.ToListAsync();

			foreach (var @event in events)
			{
				var currentTransaction = JsonSerializer.Deserialize<CreateTransactionEvent>(@event.OriginalEvent.Data.ToArray());
				await _balanceEventHandler.HandleBalanceUpdate(currentTransaction);
			}
		}
	}
}
