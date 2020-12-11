using CQRSSplitWise.Client.Query.DAL.Models;
using CQRSSplitWise.Client.Query.DAL.Repositories;
using CQRSSplitWise.DataContracts.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CQRSSplitWise.Client.Query.EventHandlers
{
	public class CreateTransactionEventHandler
	{
		private readonly IInsertRepository<Transaction> _transactionRepository;
		private readonly IQueryRepository<UserData> _userRepository;

		public CreateTransactionEventHandler(
			IInsertRepository<Transaction> transactionRepository,
			IQueryRepository<UserData> userRepository)
		{
			_transactionRepository = transactionRepository;
			_userRepository = userRepository;
		}

		public async Task HandleCreateTransactionEvent(CreateTransactionEvent createTransaction)
		{
			if (createTransaction.SourceUserID < 0
				|| createTransaction.DestUserID < 0
				|| createTransaction.Amount == 0)
			{
				return;
			}

			IEnumerable<UserData> users = null;

			var expressions = new List<Expression<Func<UserData, bool>>>
			{
				x => true
			};

			expressions.Add(x => createTransaction.DestUserID == x.UserID
				|| createTransaction.SourceUserID == x.UserID);

			users = await _userRepository.GetData(expressions);

			var sourceUser = users
				.FirstOrDefault(x => x.UserID == createTransaction.SourceUserID);

			var destUser = users
				.FirstOrDefault(x => x.UserID == createTransaction.DestUserID);

			await _transactionRepository
				.InsertData(new Transaction
				{
					SourceUserData = sourceUser,
					DestUserData = destUser,
					TransactionData = new TransactionData
					{
						Amount = createTransaction.Amount,
						Description = createTransaction.Description,
						TransactionDate = createTransaction.TransactionDate
					}
				});

			return;
		}
	}
}
