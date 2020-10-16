using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CQRSSplitWise.DAL.Read
{
	public interface IQueryRepository<TModel>
	{
		IEnumerable<TModel> GetData(Expression<Func<TModel, bool>> filterExpression);
	}
}
