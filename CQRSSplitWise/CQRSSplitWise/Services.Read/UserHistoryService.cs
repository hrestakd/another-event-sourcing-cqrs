using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper.Internal;
using CQRSSplitWise.DAL.Read;
using CQRSSplitWise.DAL.Read.Models;
using CQRSSplitWise.Filters.Read;

namespace CQRSSplitWise.Services.Read
{
	public class UserHistoryService
	{
		private readonly IQueryRepository<TransactionHistory> _repository;

		public UserHistoryService(IQueryRepository<TransactionHistory> repository)
		{
			_repository = repository;
		}

		public IEnumerable<TransactionHistory> GetUserHistory(UserHistoryFilter filter)
		{

			if (filter == null)
			{
				return _repository.GetData(x => true);
			}

			return _repository.GetData(x => true);

			//var results = _repository.GetData(x =>
			//	(filter.UserID > 0 ? x.UserData.UserID == filter.UserID : true)
			//		&&
			//	(!string.IsNullOrWhiteSpace(filter.UserName) ? x.UserData.Name.Contains(filter.UserName, StringComparison.InvariantCultureIgnoreCase) : true)
			//		&&
			//	(!string.IsNullOrWhiteSpace(filter.UserLastName) ? x.UserData.LastName.Contains(filter.UserLastName, StringComparison.InvariantCultureIgnoreCase) : true)
			//		&&
			//	(x.Transactions.Any(y => 
			//		(filter.CreatedFrom.HasValue ? y.);
			//if (filter.UserID > 0)
			//{
			//	expressions.Add(x => x.UserData.UserID == filter.UserID);
			//}

			//if (!string.IsNullOrWhiteSpace(filter.UserName))
			//{
			//	expressions.Add(x => x.UserData.Name.Contains(filter.UserName, StringComparison.InvariantCultureIgnoreCase));
			//}

			//if (!string.IsNullOrWhiteSpace(filter.UserLastName))
			//{
			//	expressions.Add(x => x.UserData.LastName.Contains(filter.UserLastName, StringComparison.InvariantCultureIgnoreCase));
			//}

			//Expression finalUserExpression = expressions.First();
			//foreach (var exp in expressions)
			//{
			//	finalUserExpression = Expression.Add(finalUserExpression, exp);
			//}

			//Expression<Func<Transaction, bool>> transactionBaseExpression = x => true;
			//var transactionExpressions = new List<Expression<Func<Transaction, bool>>>();
			//transactionExpressions.Add(transactionBaseExpression);

			//if (filter.AmountRangeFilterSet)
			//{
			//	transactionExpressions.Add(x =>
			//		filter.AmountFrom.HasValue ? x.Amount >= filter.AmountFrom : true
			//			&&
			//		filter.AmountTo.HasValue ? x.Amount < filter.AmountTo : true
			//	);
			//}

			//if (filter.DateRangeFilterSet)
			//{
			//	transactionExpressions.Add(x =>
			//		filter.CreatedFrom.HasValue ? x.TransactionDate >= filter.CreatedFrom : true
			//			&&
			//		filter.CreatedTo.HasValue ? x.TransactionDate < filter.CreatedTo : true
			//	);
			//}

			//Expression<Func<Transaction, bool>> compiledTransactionExpression = x => true;
			//Expression<Func<Transaction, bool>> finalTransactionExpression = transactionExpressions.First();
			//foreach (var exp in transactionExpressions.Skip(1))
			//{
			//	//finalTransactionExpression = Expression.And(finalTransactionExpression, exp);
			//	var func = finalTransactionExpression.Compile();
			//	var func2 = exp.Compile();
			//	func.
			//}

			//finalTransactionExpression.

			//Expression<Func<UserHistory, bool>> userTransactionFilter = x => x.Transactions.Any( finalTransactionExpression);
			//Expression.And()

			//var userFunc = finalUserExpression.GetChain().Where((UserHistory)x => x.;
			//finalUserExpression = Expression.MakeMemberAccess(finalTransactionExpression, new MemberInfo);
			//finalUserExpression.

			//_repository.GetData(baseExpression);
		}
	}
}
