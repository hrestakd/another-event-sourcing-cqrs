using CQRSSplitWise.Client.Query.DAL.Models;
using CQRSSplitWise.Client.Query.DAL.Repositories;
using CQRSSplitWise.Client.Query.DTO;
using CQRSSplitWise.Client.Query.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CQRSSplitWise.Client.Query.Services
{
	public class TransactionService
	{
		private readonly IQueryRepository<Transaction> _transactionRepository;

		public TransactionService(IQueryRepository<Transaction> repository)
		{
			_transactionRepository = repository;
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
