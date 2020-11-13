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
		Task<IEnumerable<TModel>> GetData(IEnumerable<Expression<Func<TModel, bool>>> filterExpressions);
	}
}
