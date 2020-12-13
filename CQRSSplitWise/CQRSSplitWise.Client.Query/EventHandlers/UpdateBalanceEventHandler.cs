﻿using CQRSSplitWise.Client.Query.DAL.Models;
using CQRSSplitWise.Client.Query.DAL.Repositories;
using CQRSSplitWise.DataContracts.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CQRSSplitWise.Client.Query.EventHandlers
{
	public class UpdateBalanceEventHandler
	{
		private readonly IInsertRepository<Transaction> _transactionRepository;
		private readonly IQueryRepository<UserData> _userRepository;
		private readonly IInsertRepository<UserBalance> _userBalanceInsertRepository;
		private readonly IQueryRepository<UserBalance> _userBalanceQueryRepository;

		public UpdateBalanceEventHandler(
			IInsertRepository<Transaction> transactionRepository,
			IQueryRepository<UserData> userRepository,
			IInsertRepository<UserBalance> userBalanceRepository,
			IQueryRepository<UserBalance> userBalanceQueryRepository)
		{
			_transactionRepository = transactionRepository;
			_userRepository = userRepository;
			_userBalanceInsertRepository = userBalanceRepository;
			_userBalanceQueryRepository = userBalanceQueryRepository;
		}

		public async Task HandleBalanceUpdate(CreateTransactionEvent createTransaction)
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

			var balanceExpressions = new List<Expression<Func<UserBalance, bool>>>
			{
				x => true
			};

			// We need to update both or insert if these are their first payments
			balanceExpressions.Add(x =>
				createTransaction.SourceUserID == x.SourceUserData.UserID
					&& createTransaction.DestUserID == x.DestUserData.UserID
				||
				createTransaction.DestUserID == x.SourceUserData.UserID
					&& createTransaction.SourceUserID == x.DestUserData.UserID);

			var userBalancesForUpdate = await _userBalanceQueryRepository
				.GetData(balanceExpressions);

			var balancesForInsert = new List<UserBalance>();

			// Handle source user balance
			if (userBalancesForUpdate.Any(x => x.SourceUserData.UserID == sourceUser.UserID))
			{
				var currentBalance = userBalancesForUpdate
					.Where(x => x.SourceUserData.UserID == sourceUser.UserID
						&& x.DestUserData.UserID == destUser.UserID)
					.Select(x => x.TotalBalance)
					.FirstOrDefault();

				var updateBalance = new UserBalance
				{
					SourceUserData = sourceUser,
					DestUserData = destUser,
					TotalBalance = currentBalance + createTransaction.Amount
				};

				await _userBalanceInsertRepository.UpdateData(updateBalance);
			}
			else
			{
				var userBalance = new UserBalance
				{
					SourceUserData = sourceUser,
					DestUserData = destUser,
					TotalBalance = createTransaction.Amount,
				};

				balancesForInsert.Add(userBalance);
			}

			// Handle target user balance
			if (userBalancesForUpdate.Any(x => x.SourceUserData.UserID == destUser.UserID))
			{
				var currentBalance = userBalancesForUpdate
					.Where(x => x.SourceUserData.UserID == destUser.UserID
						&& x.DestUserData.UserID == sourceUser.UserID)
					.Select(x => x.TotalBalance)
					.FirstOrDefault();

				var updateBalance = new UserBalance
				{
					SourceUserData = destUser,
					DestUserData = sourceUser,
					TotalBalance = currentBalance - createTransaction.Amount
				};

				await _userBalanceInsertRepository.UpdateData(updateBalance);
			}
			else
			{
				var userBalance = new UserBalance
				{
					SourceUserData = destUser,
					DestUserData = sourceUser,
					TotalBalance = createTransaction.Amount * (-1),
				};

				balancesForInsert.Add(userBalance);
			}

			if (balancesForInsert.Any())
			{
				await _userBalanceInsertRepository.InsertData(balancesForInsert);
			}
		}
	}
}
