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
	public class TransactionService
	{
		private readonly IQueryRepository<Transaction> _transactionRepository;
		private readonly EventStoreClient _eventStoreClient;
		private readonly CreateTransactionEventHandler _createTransactionHandler;

		public TransactionService(
			IQueryRepository<Transaction> repository,
			EventStoreClient eventStoreClient,
			CreateTransactionEventHandler createTransactionEventHandler)
		{
			_transactionRepository = repository;
			_eventStoreClient = eventStoreClient;
			_createTransactionHandler = createTransactionEventHandler;
		}

		public async Task<IEnumerable<TransactionDTO>> GetTransactions(TransactionFilter filter)
		{
			var expressions = GenerateExpressions(filter);

			IEnumerable<Transaction> transactionData;

			if (expressions == null || expressions.Count == 0)
			{
				transactionData = await _transactionRepository.GetData(null);
			}
			else
			{
				transactionData = await _transactionRepository.GetData(expressions);
			}

			var data = new List<TransactionDTO>();
			foreach (var transaction in transactionData)
			{
				data.Add(new TransactionDTO
				{
					Amount = transaction.TransactionData.Amount,
					Description = transaction.TransactionData.Description,
					TransactionDate = transaction.TransactionData.TransactionDate,
					SourceUser = new UserDTO
					{
						FirstName = transaction.SourceUserData.Name,
						LastName = transaction.SourceUserData.LastName,
						UserID = transaction.SourceUserData.UserID
					},
					TargetUser = new UserDTO
					{
						FirstName = transaction.DestUserData.Name,
						LastName = transaction.DestUserData.LastName,
						UserID = transaction.DestUserData.UserID
					}
				});
			}

			return data;
		}

		public async Task RebuildTransactions()
		{
			var fullStream = _eventStoreClient
				.ReadStreamAsync(
					Direction.Forwards,
					EventStreams.Transactions.ToString(),
					StreamPosition.Start);

			await fullStream.ForEachAsync(
				async x =>
				{
					var @event = JsonSerializer.Deserialize<CreateTransactionEvent>(x.Event.Data.ToArray());
					await _createTransactionHandler.HandleCreateTransactionEvent(@event);
				});
		}

		private List<Expression<Func<Transaction, bool>>> GenerateExpressions(TransactionFilter filter)
		{
			var expressions = new List<Expression<Func<Transaction, bool>>>
			{
				x => true
			};

			if (filter == null)
			{
				return expressions;
			}

			if (filter.AllForUserID > 0)
			{
				expressions.Add(x => x.SourceUserData.UserID == filter.AllForUserID
					|| x.DestUserData.UserID == filter.AllForUserID);
			}

			if (filter.PaidByUserID > 0)
			{
				expressions.Add(x => x.SourceUserData.UserID == filter.PaidByUserID);
			}

			if (filter.PaidToUserID > 0)
			{
				expressions.Add(x => x.DestUserData.UserID == filter.PaidToUserID);
			}

			if (filter.AmountFrom.HasValue)
			{
				expressions.Add(x => x.TransactionData.Amount >= filter.AmountFrom);
			}

			if (filter.AmountTo.HasValue)
			{
				expressions.Add(x => x.TransactionData.Amount < filter.AmountTo);
			}

			if (filter.CreatedFrom.HasValue)
			{
				expressions.Add(x => x.TransactionData.TransactionDate >= filter.CreatedFrom.Value);
			}

			if (filter.CreatedTo.HasValue)
			{
				expressions.Add(x => x.TransactionData.TransactionDate < filter.CreatedTo.Value);
			}

			return expressions;
		}
	}
}
