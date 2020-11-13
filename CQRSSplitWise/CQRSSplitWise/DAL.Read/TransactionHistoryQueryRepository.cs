using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CQRSSplitWise.Config;
using CQRSSplitWise.DAL.Read.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CQRSSplitWise.DAL.Read
{
	public class TransactionHistoryQueryRepository : IQueryRepository<TransactionHistory>
	{
		private readonly IMongoCollection<TransactionHistory> _transactionHistory;

		public TransactionHistoryQueryRepository(TransactionHistoryDBSettings config)
		{
			var client = new MongoClient(config.ConnectionString);
			var db = client.GetDatabase(config.DatabaseName);

			_transactionHistory = db.GetCollection<TransactionHistory>(config.TransactionHistoryCollectionName);
		}

		public async Task<IEnumerable<TransactionHistory>> GetData(IEnumerable<Expression<Func<TransactionHistory, bool>>> filterExpressions)
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

		public async Task<TransactionHistory> InsertData(TransactionHistory model)
		{
			await _transactionHistory.InsertOneAsync(model);

			return model;
		}
	}
}
