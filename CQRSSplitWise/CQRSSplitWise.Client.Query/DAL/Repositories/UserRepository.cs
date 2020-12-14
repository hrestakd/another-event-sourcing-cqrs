﻿using CQRSSplitWise.Client.Query.Config;
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
	public class UserRepository: IRepository<UserData>
	{
		private readonly IMongoCollection<UserData> _users;

		public UserRepository(MongoDBSettings config)
		{
			var client = new MongoClient(config.ConnectionString);
			var db = client.GetDatabase(config.DatabaseName);

			_users = db.GetCollection<UserData>(config.UsersCollectionName);
		}

		public async Task DropCollection()
		{
			await _users.DeleteManyAsync(x => true);
		}

		public async Task<IEnumerable<UserData>> GetData(IEnumerable<Expression<Func<UserData, bool>>> filterExpressions)
		{
			var usersExpressions = _users.AsQueryable();

			if (filterExpressions == null || filterExpressions.Count() == 0)
			{
				return await Task.FromResult(usersExpressions.AsEnumerable());
			}

			foreach (var exp in filterExpressions)
			{
				usersExpressions = usersExpressions.Where(exp);
			}

			var users = await Task.FromResult(usersExpressions.AsEnumerable());

			return users;
		}

		public async Task<UserData> InsertData(UserData model)
		{
			await _users.InsertOneAsync(model);

			return model;
		}

		public async Task<IEnumerable<UserData>> InsertData(IEnumerable<UserData> data)
		{
			await _users.InsertManyAsync(data);

			return data;
		}

		public Task UpdateData(UserData data)
		{
			throw new NotImplementedException();
		}
	}
}
