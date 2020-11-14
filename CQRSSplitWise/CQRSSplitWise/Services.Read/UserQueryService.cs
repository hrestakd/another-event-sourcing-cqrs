using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Internal;
using CQRSSplitWise.DAL.Read;
using CQRSSplitWise.DAL.Read.Models;
using CQRSSplitWise.DAL.Read.Views;
using CQRSSplitWise.DTO.Read;
using CQRSSplitWise.Filters.Read;

namespace CQRSSplitWise.Services.Read
{
	public class UserQueryService
	{
		private readonly IQueryRepository<TransactionHistory> _repository;
		private readonly IQueryRepository<UserStatusView> _userStatusRepository;
		private readonly IMapper _mapper;

		public UserQueryService(IQueryRepository<TransactionHistory> repository,
			IMapper mapper,
			IQueryRepository<UserStatusView> userStatusRepository)
		{
			_repository = repository;
			_userStatusRepository = userStatusRepository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<UserHistoryDTO>> GetUserHistory(UserHistoryFilter filter)
		{
			var expressions = GenerateExpressions(filter);

			IEnumerable<TransactionHistory> transactionData;

			if (expressions == null || expressions.Count == 0)
			{
				transactionData = await _repository.GetData(null);
			}
			else
			{
				transactionData = await _repository.GetData(expressions);
			}

			var data = new List<UserHistoryDTO>();
			foreach (var item in transactionData)
			{
				data.Add(_mapper.Map<UserHistoryDTO>(item));
			}

			return data;
		}

		public async Task<UserStatusDTO> GetUserState(int userID)
		{
			var expressions = new List<Expression<Func<UserStatusView, bool>>>
			{
				x => x.SourceUserData.UserID == userID
					|| x.DestUserData.UserID == userID
			};

			var userData = await _userStatusRepository.GetData(expressions);

			if (!userData.Any())
			{
				return new UserStatusDTO();
			}

			// We need to produce set of information relative to target user
			// Because it is possible that user only received the payment and never actually paid anything
			var finalBalancePerUser = new Dictionary<int, BalanceForUser>();
			foreach (var record in userData)
			{
				BalanceForUser currentBalance = null;
				var currentID = 0;
				if (record.SourceUserData.UserID == userID)
				{
					// If source user is the target user, we add amount to final balance
					currentBalance = new BalanceForUser
					{
						Balance = record.UserBalance,
						LastName = record.DestUserData.LastName,
						Name = record.DestUserData.Name
					};
					currentID = record.DestUserData.UserID;
				}
				else
				{
					// Otherwise, we need to invert the amount
					currentBalance = new BalanceForUser
					{
						Balance = record.UserBalance * -1,
						LastName = record.SourceUserData.LastName,
						Name = record.SourceUserData.Name
					};
					currentID = record.SourceUserData.UserID;
				}

				if (finalBalancePerUser.ContainsKey(currentID))
				{
					finalBalancePerUser[currentID].Balance += currentBalance.Balance;
				}
				else
				{
					finalBalancePerUser[currentID] = currentBalance;
				}
			}

			// Get target user information
			var targetUserInformation = userData
				.Where(x => x.SourceUserData.UserID == userID
					|| x.DestUserData.UserID == userID)
				.Select(x => x.SourceUserData.UserID == userID
					? new
					{
						Name = x.SourceUserData.Name,
						LastName = x.SourceUserData.LastName
					}
					: new
					{
						Name = x.DestUserData.Name,
						LastName = x.DestUserData.LastName
					})
				.FirstOrDefault();

			var userStatus = new UserStatusDTO
			{
				Name = targetUserInformation.Name,
				LastName = targetUserInformation.LastName,
				Balances = finalBalancePerUser.Values
			};

			return userStatus;
		}

		private List<Expression<Func<TransactionHistory, bool>>> GenerateExpressions(UserHistoryFilter filter)
		{
			var expressions = new List<Expression<Func<TransactionHistory, bool>>>
			{
				x => true
			};

			if (filter == null)
			{
				return expressions;
			}

			if (filter.UserID > 0)
			{
				expressions.Add(x => x.SourceUserData.UserID == filter.UserID);
			}

			if (!string.IsNullOrWhiteSpace(filter.UserName))
			{
				expressions.Add(x => x.SourceUserData.Name.Contains(filter.UserName, StringComparison.InvariantCultureIgnoreCase));
			}

			if (!string.IsNullOrWhiteSpace(filter.UserLastName))
			{
				expressions.Add(x => x.SourceUserData.LastName.Contains(filter.UserLastName, StringComparison.InvariantCultureIgnoreCase));
			}

			if (filter.AmountFrom > 0)
			{
				expressions.Add(x => x.TransactionData.Amount >= filter.AmountFrom);
			}

			if (filter.AmountTo > 0)
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

			if (filter.TransactionType > 0)
			{
				expressions.Add(x => x.TransactionData.TransactionType == filter.TransactionType);
			}

			return expressions;
		}
	}
}
