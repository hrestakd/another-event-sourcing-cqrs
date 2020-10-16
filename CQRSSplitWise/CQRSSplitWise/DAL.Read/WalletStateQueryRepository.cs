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
	public class WalletStateQueryRepository : IQueryRepository<WalletState>
	{
		private readonly IMongoCollection<WalletState> _walletState;

		public WalletStateQueryRepository(WalletStateDBSettings config)
		{
			var client = new MongoClient(config.ConnectionString);
			var db = client.GetDatabase(config.DatabaseName);

			_walletState = db.GetCollection<WalletState>(config.WalletStateCollectionName);
		}

		public IEnumerable<WalletState> GetData(Expression<Func<WalletState, bool>> filterExpression)
		{
			var state = _walletState.Find(filterExpression).ToEnumerable();

			return state;
		}
	}
}
