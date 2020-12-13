using System.Collections.Generic;
using System.Threading.Tasks;

namespace CQRSSplitWise.Client.Query.DAL.Repositories
{
	public interface IInsertRepository<TModel>
	{
		Task<TModel> InsertData(TModel data);

		Task<IEnumerable<TModel>> InsertData(IEnumerable<TModel> data);

		Task UpdateData(TModel data);
	}
}
