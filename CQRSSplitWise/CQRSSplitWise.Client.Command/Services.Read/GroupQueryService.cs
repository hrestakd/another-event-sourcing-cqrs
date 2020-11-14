using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using CQRSSplitWise.DAL.Read;
using CQRSSplitWise.DAL.Read.Models;
using CQRSSplitWise.DTO.Read;
using CQRSSplitWise.Filters.Read;

namespace CQRSSplitWise.Services.Read
{
	public class GroupQueryService
	{
		private readonly IQueryRepository<TransactionHistory> _repository;
		private readonly IMapper _mapper;

		public GroupQueryService(IQueryRepository<TransactionHistory> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<GroupHistoryDTO>> GetGroupHistory(GroupHistoryFilter filter)
		{
			var expressions = await GenerateExpressions(filter);

			IEnumerable<TransactionHistory> transactionData;

			if (expressions == null || expressions.Count() == 0)
			{
				transactionData = await _repository.GetData(null);
			}
			else
			{
				transactionData = await _repository.GetData(expressions);
			}

			var data = _mapper.Map<IEnumerable<GroupHistoryDTO>>(transactionData);

			return data;
		}

		public async Task GetGroupState(int groupID)
		{
			return;
		}

		private async Task<IEnumerable<Expression<Func<TransactionHistory, bool>>>> GenerateExpressions(GroupHistoryFilter filter)
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

			return await Task.FromResult(expressions);
		}
	}
}
