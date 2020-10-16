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
	public class GroupStateQueryRepository : IQueryRepository<GroupState>
	{
		private readonly IMongoCollection<GroupState> _groupState;

		public GroupStateQueryRepository(GroupStateDBSettings config)
		{
			var client = new MongoClient(config.ConnectionString);
			var db = client.GetDatabase(config.DatabaseName);

			_groupState = db.GetCollection<GroupState>(config.GroupStateCollectionName);
		}

		public IEnumerable<GroupState> GetData(Expression<Func<GroupState, bool>> filterExpression)
		{
			var state = _groupState.Find(filterExpression).ToEnumerable();

			return state;
		}
	}
}
