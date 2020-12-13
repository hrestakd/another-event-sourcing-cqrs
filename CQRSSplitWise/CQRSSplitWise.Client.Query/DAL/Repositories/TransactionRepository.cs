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
	public class TransactionRepository : IQueryRepository<Transaction>, IInsertRepository<Transaction>
	{
		private readonly IMongoCollection<Transaction> _transactionHistory;

		public TransactionRepository(MongoDBSettings config)
		{
			var client = new MongoClient(config.ConnectionString);
			var db = client.GetDatabase(config.DatabaseName);

			_transactionHistory = db.GetCollection<Transaction>(config.TransactionsCollectionName);
		}

		public async Task<IEnumerable<Transaction>> GetData(IEnumerable<Expression<Func<Transaction, bool>>> filterExpressions)
		{
			var transactionsExpression = _transactionHistory.AsQueryable();

			if (filterExpressions == null || filterExpressions.Count() == 0)
			{
				return await Task.FromResult(transactionsExpression.AsEnumerable());
			}

			foreach (var exp in filterExpressions)
			{
				transactionsExpression = transactionsExpression.Where(exp);
			}

			var history = await Task.FromResult(transactionsExpression.AsEnumerable());

			return history;
		}

		public async Task<Transaction> InsertData(Transaction model)
		{
			await _transactionHistory.InsertOneAsync(model);

			return model;
		}

		public async Task<IEnumerable<Transaction>> InsertData(IEnumerable<Transaction> data)
		{
			await _transactionHistory.InsertManyAsync(data);

			return data;
		}

		public Task UpdateData(Transaction data)
		{
			throw new NotImplementedException();
		}
	}
}
