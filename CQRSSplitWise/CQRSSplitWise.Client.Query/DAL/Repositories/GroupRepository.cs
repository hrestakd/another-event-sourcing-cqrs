using CQRSSplitWise.Client.Query.Config;
using CQRSSplitWise.Client.Query.DAL.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CQRSSplitWise.Client.Query.DAL.Repositories
{
	public class GroupRepository : IRepository<GroupData>
	{
		private readonly IMongoCollection<GroupData> _groups;

		public GroupRepository(MongoDBSettings config)
		{
			var client = new MongoClient(config.ConnectionString);
			var db = client.GetDatabase(config.DatabaseName);

			_groups = db.GetCollection<GroupData>(config.GroupsCollectionName);
		}

		public async Task DropCollection()
		{
			await _groups.DeleteManyAsync(x => true);
		}

		public async Task<IEnumerable<GroupData>> GetData(IEnumerable<Expression<Func<GroupData, bool>>> filterExpressions)
		{
			var groupsExpressions = _groups.AsQueryable();

			if (filterExpressions == null || filterExpressions.Count() == 0)
			{
				return await Task.FromResult(groupsExpressions.AsEnumerable());
			}

			foreach (var exp in filterExpressions)
			{
				groupsExpressions = groupsExpressions.Where(exp);
			}

			var users = await Task.FromResult(groupsExpressions.AsEnumerable());

			return users;
		}

		public async Task<GroupData> InsertData(GroupData model)
		{
			await _groups.InsertOneAsync(model);

			return model;
		}

		public async Task<IEnumerable<GroupData>> InsertData(IEnumerable<GroupData> data)
		{
			await _groups.InsertManyAsync(data);

			return data;
		}

		public async Task UpdateData(GroupData data)
		{
			var targetGroupUsers = await _groups
				.AsQueryable()
				.Where(x => x.GroupID == data.GroupID)
				.Select(x => x.UsersInGroup
					.Select(y => y.UserID))
				.FirstOrDefaultAsync();

			var newUsers = data.UsersInGroup
				.Where(x => !targetGroupUsers.Contains(x.UserID));

			await _groups.UpdateOneAsync(
				x => x.GroupID == data.GroupID,
				Builders<GroupData>.Update.PushEach(
					y => y.UsersInGroup,
					newUsers));
		}
	}
}
