using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CQRSSplitWise.DAL.Read;
using CQRSSplitWise.DAL.Read.Models;
using CQRSSplitWise.DTO.Read;
using CQRSSplitWise.Filters.Read;

namespace CQRSSplitWise.Services.Read
{
	public class GroupHistoryService
	{
		private readonly IQueryRepository<TransactionHistory> _repository;

		public GroupHistoryService(IQueryRepository<TransactionHistory> repository)
		{
			_repository = repository;
		}

		public IEnumerable<GroupHistoryDTO> GetUserHistory(GroupHistoryFilter filter)
		{
			var expressions = GenerateExpressions(filter);

			IEnumerable<TransactionHistory> transactionData;

			if (expressions == null || expressions.Count == 0)
			{
				transactionData = _repository.GetData(null);
			}
			else
			{
				transactionData = _repository.GetData(expressions);
			}

			var data = MapToGroupHistory(transactionData);

			return data;
		}

		IEnumerable<GroupHistoryDTO> MapToGroupHistory(IEnumerable<TransactionHistory> transactionHistory)
		{
			var groupHistory = transactionHistory
				.Select(x => new GroupHistoryDTO
				{
					Name = x.GroupData.GroupName,
					Amount = x.TransactionData.Amount,
					Description = x.TransactionData.Description,
					DestWalletName = x.TransactionData.DestWalletName,
					SourceWalletName = x.TransactionData.SourceWalletName,
					TransactionDate = x.TransactionData.TransactionDate,
					TransactionType = x.TransactionData.TransactionType
				})
				.AsEnumerable();

			return groupHistory;
		}

		private List<Expression<Func<TransactionHistory, bool>>> GenerateExpressions(GroupHistoryFilter filter)
		{
			var expressions = new List<Expression<Func<TransactionHistory, bool>>>
			{
				x => true
			};

			if (filter == null)
			{
				return expressions;
			}

			if (filter.GroupID > 0)
			{
				expressions.Add(x => x.GroupData.GroupID == filter.GroupID);
			}

			if (!string.IsNullOrWhiteSpace(filter.GroupName))
			{
				expressions.Add(x => x.GroupData.GroupName.Contains(filter.GroupName, StringComparison.InvariantCultureIgnoreCase));
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
