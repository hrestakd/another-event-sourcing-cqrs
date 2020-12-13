using AutoMapper;
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
	public class GroupQueryService
	{
		private readonly IRepository<Transaction> _repository;
		private readonly IMapper _mapper;

		public GroupQueryService(IRepository<Transaction> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<GroupHistoryDTO>> GetGroupHistory(GroupHistoryFilter filter)
		{
			var expressions = await GenerateExpressions(filter);

			IEnumerable<Transaction> transactionData;

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

		private async Task<IEnumerable<Expression<Func<Transaction, bool>>>> GenerateExpressions(GroupHistoryFilter filter)
		{
			var expressions = new List<Expression<Func<Transaction, bool>>>
			{
				x => true
			};

			if (filter == null)
			{
				return expressions;
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

			return await Task.FromResult(expressions);
		}
	}
}
