using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CQRSSplitWise.DAL.Read.Models;

namespace CQRSSplitWise.DAL.Read
{
	public interface IQueryRepository<TModel>
	{
		IEnumerable<TModel> GetData(List<Expression<Func<TransactionHistory, bool>>> filterExpressions);
	}
}
