using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSSplitWise.Config;
using CQRSSplitWise.DAL.Read.Models;
using MongoDB.Driver;

namespace CQRSSplitWise.DAL.Read
{
	public class GroupStateQueryRepository : IQueryRepository
	{
		private readonly IMongoCollection<GroupState> _groupState;

		public GroupStateQueryRepository(GroupStateDBSettings config)
		{
			var client = new MongoClient(config.ConnectionString);
			var db = client.GetDatabase(config.DatabaseName);

			_groupState = db.GetCollection<GroupState>(config.GroupStateCollectionName);
		}
	}
}
