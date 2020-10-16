using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CQRSSplitWise.Config;
using CQRSSplitWise.DAL.Read.Models;
using MongoDB.Driver;

namespace CQRSSplitWise.DAL.Read
{
	public class TransactionHistoryQueryRepository : IQueryRepository<TransactionHistory>
	{
		private readonly IMongoCollection<TransactionHistory> _groupHistory;

		public TransactionHistoryQueryRepository(TransactionHistoryDBSettings config)
		{
			var client = new MongoClient(config.ConnectionString);
			var db = client.GetDatabase(config.DatabaseName);

			_groupHistory = db.GetCollection<TransactionHistory>(config.TransactionHistoryCollectionName);
		}

		public IEnumerable<TransactionHistory> GetData(Expression<Func<TransactionHistory, bool>> filterExpression)
		{
			var history = _groupHistory.Find(filterExpression).ToEnumerable();

			return history;
		}
	}
}
