using CQRSSplitWise.Config;
using CQRSSplitWise.DAL.Read.Models;
using CQRSSplitWise.DAL.Read.Views;
using CQRSSplitWise.Models.Enums;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CQRSSplitWise.DAL.Read
{
	public class UserStatusViewRepository : IQueryRepository<UserStatusView>
	{
		private readonly string _userStateViewName = "UserState";

		private readonly IMongoCollection<UserStatusView> _userStatusView;

		public UserStatusViewRepository(TransactionHistoryDBSettings config)
		{
			var client = new MongoClient(config.ConnectionString);
			var db = client.GetDatabase(config.DatabaseName);

			if (!db.ListCollectionNames().ToEnumerable().Any(x => x == _userStateViewName))
			{
				var transactionCollection = db.GetCollection<TransactionHistory>(config.TransactionHistoryCollectionName);

				var groupByPayerAggregator = transactionCollection
					.Aggregate()
					.Project(x => new
					{
						PayerData = x.SourceUserData,
						PayeeData = x.DestUserData,
						TransactionAmount = x.TransactionData.TransactionType == TransactionType.Refund
							? (x.TransactionData.Amount * -1)
							: x.TransactionData.Amount
					})
					.Group(x => new
					{
						Payer = x.PayerData,
						Payee = x.PayeeData
					},
					x => new
					{
						Key = x.Key,
						Sum = x.Sum(y => y.TransactionAmount)
					})
					.Project(x => new UserStatusView
					{
						SourceUserData = new Views.UserData
						{
							UserID = x.Key.Payer.UserID,
							Name = x.Key.Payer.Name,
							LastName = x.Key.Payer.LastName
						},
						DestUserData = new Views.UserData
						{
							UserID = x.Key.Payee.UserID,
							Name = x.Key.Payee.Name,
							LastName = x.Key.Payee.LastName
						},
						UserBalance = x.Sum
					});

				var payerViewPipeline = PipelineDefinition<TransactionHistory, UserStatusView>.Create(groupByPayerAggregator.Stages);

				db.CreateView(_userStateViewName, config.TransactionHistoryCollectionName, payerViewPipeline);
			}

			_userStatusView = db.GetCollection<UserStatusView>(_userStateViewName);
		}

		public async Task<IEnumerable<UserStatusView>> GetData(IEnumerable<Expression<Func<UserStatusView, bool>>> filterExpressions)
		{
			var userStatusExpression = _userStatusView.AsQueryable();

			if (filterExpressions == null || filterExpressions.Count() == 0)
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
	}
}
