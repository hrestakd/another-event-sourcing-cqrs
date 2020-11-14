using System.Threading.Tasks;

namespace CQRSSplitWise.Client.Query.DAL.Repositories
{
	public interface IInsertRepository<TModel>
	{
		Task<TModel> InsertData(TModel data);
	}
}
