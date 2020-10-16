﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CQRSSplitWise.Config;
using CQRSSplitWise.DAL.Read.Models;
using MongoDB.Driver;

namespace CQRSSplitWise.DAL.Read
{
	public class GroupHistoryQueryRepository : IQueryRepository<GroupHistory>
	{
		private readonly IMongoCollection<GroupHistory> _groupHistory;

		public GroupHistoryQueryRepository(GroupHistoryDBSettings config)
		{
			var client = new MongoClient(config.ConnectionString);
			var db = client.GetDatabase(config.DatabaseName);

			_groupHistory = db.GetCollection<GroupHistory>(config.GroupHistoryCollectionName);
		}

		public IEnumerable<GroupHistory> GetData(Expression<Func<GroupHistory, bool>> filterExpression)
		{
			var history = _groupHistory.Find(filterExpression).ToEnumerable();

			return history;
		}
	}
}
