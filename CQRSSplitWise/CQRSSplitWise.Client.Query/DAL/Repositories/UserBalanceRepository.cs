using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using CQRSSplitWise.Client.Query.Config;
using CQRSSplitWise.Client.Query.DAL.Models;
using CQRSSplitWise.DataContracts.Enums;

namespace CQRSSplitWise.Client.Query.DAL.Repositories
{
	public class UserBalanceRepository : IRepository<UserBalance>
	{
		private readonly IMongoCollection<UserBalance> _usersBalance;

		public UserBalanceRepository(MongoDBSettings config)
		{
			var client = new MongoClient(config.ConnectionString);
			var db = client.GetDatabase(config.DatabaseName);

			_usersBalance = db.GetCollection<UserBalance>(config.UsersBalanceCollectionName);
		}

		public async Task DropCollection()
		{
			await _usersBalance.DeleteManyAsync(x => true);
		}

		public async Task<IEnumerable<UserBalance>> GetData(IEnumerable<Expression<Func<UserBalance, bool>>> filterExpressions)
		{
			var userStatusExpression = _usersBalance.AsQueryable();

			if (filterExpressions is null || !filterExpressions.Any())
			{
				return await Task.FromResult(userStatusExpression.AsEnumerable());
			}

			foreach (var exp in filterExpressions)
			{
				userStatusExpression = userStatusExpression.Where(exp);
			}

			var results = await Task.FromResult(userStatusExpression.AsEnumerable());

			return results;
		}

		public async Task<UserBalance> InsertData(UserBalance data)
		{
			await _usersBalance.InsertOneAsync(data);

			return data;
		}

		public async Task<IEnumerable<UserBalance>> InsertData(IEnumerable<UserBalance> data)
		{
			await _usersBalance.InsertManyAsync(data);

			return data;
		}

		public async Task UpdateData(UserBalance data)
		{
			// Update the total balance for source user
			await _usersBalance.UpdateOneAsync(
				x => x.SourceUserData.ID == data.SourceUserData.ID
					&& x.DestUserData.ID == data.DestUserData.ID,
				Builders<UserBalance>.Update.Set(x => x.TotalBalance, data.TotalBalance));
		}
	}
}
