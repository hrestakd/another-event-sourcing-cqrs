using CQRSSplitWise.Config;
using CQRSSplitWise.DAL.Read.Models;
using MongoDB.Driver;

namespace CQRSSplitWise.DAL.Read
{
	public class UserHistoryQueryRepository : IQueryRepository
	{
		private readonly IMongoCollection<UserHistory> _userHistory;

		public UserHistoryQueryRepository(UserHistoryDBSettings config)
		{
			var client = new MongoClient(config.ConnectionString);
			var db = client.GetDatabase(config.DatabaseName);

			_userHistory = db.GetCollection<UserHistory>(config.UserHistoryCollectionName);
		}
	}
}
