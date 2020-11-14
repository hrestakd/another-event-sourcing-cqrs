using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CQRSSplitWise.Client.Query.DAL.Repositories
{
	public interface IQueryRepository<TModel>
	{
		Task<IEnumerable<TModel>> GetData(IEnumerable<Expression<Func<TModel, bool>>> filterExpressions);
	}
}
