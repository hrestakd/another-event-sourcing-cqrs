using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CQRSSplitWise.Client.Query.DAL.Repositories
{
	public interface IRepository<TModel>
	{
		Task<IEnumerable<TModel>> GetData(IEnumerable<Expression<Func<TModel, bool>>> filterExpressions);

		Task<TModel> InsertData(TModel data);

		Task<IEnumerable<TModel>> InsertData(IEnumerable<TModel> data);

		Task UpdateData(TModel data);

		Task DropCollection();
	}
}
