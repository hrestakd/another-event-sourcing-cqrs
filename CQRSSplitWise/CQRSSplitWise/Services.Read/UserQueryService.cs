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
using CQRSSplitWise.DTO.Read;
using CQRSSplitWise.Filters.Read;

namespace CQRSSplitWise.Services.Read
{
	public class UserQueryService
	{
		private readonly IQueryRepository<TransactionHistory> _repository;
		private readonly IMapper _mapper;

		public UserQueryService(IQueryRepository<TransactionHistory> repository, IMapper mapper)
		{
			_repository = repository;
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

			var data = _mapper.Map<IEnumerable<UserHistoryDTO>>(transactionData);

			return data;
		}

		public async Task GetUserState(int userID)
		{
			return;
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
				expressions.Add(x => x.UserData.UserID == filter.UserID);
			}

			if (!string.IsNullOrWhiteSpace(filter.UserName))
			{
				expressions.Add(x => x.UserData.Name.Contains(filter.UserName, StringComparison.InvariantCultureIgnoreCase));
			}

			if (!string.IsNullOrWhiteSpace(filter.UserLastName))
			{
				expressions.Add(x => x.UserData.LastName.Contains(filter.UserLastName, StringComparison.InvariantCultureIgnoreCase));
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
