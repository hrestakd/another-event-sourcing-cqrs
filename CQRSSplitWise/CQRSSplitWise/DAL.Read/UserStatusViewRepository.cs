using CQRSSplitWise.Config;
using CQRSSplitWise.DAL.Read.Models;
using CQRSSplitWise.DAL.Read.Views;
using CQRSSplitWise.Models.Enums;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
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

			var transactionCollection = db.GetCollection<TransactionHistory>(config.TransactionHistoryCollectionName);

			var transactionQUery = transactionCollection.AsQueryable();

			var payerUserIDs = transactionQUery.
				Select(x => x.SourceUserData.UserID)
				.ToHashSet();

			var usersWithoutPaymentIDs = transactionQUery
				.Where(x => !payerUserIDs.Contains(x.DestUserData.UserID))
				.Select(x=>x.DestUserData.UserID)
				.ToHashSet();

			var groupByPayerAggregator = transactionCollection
				.Aggregate()
				.Group(x => new { PayerID = x.SourceUserData.UserID, PayeeID = x.DestUserData.UserID },
					x => new
					{
						Key = x.Key,
						TransactionSum = x.Sum(z => z.TransactionData.TransactionType == TransactionType.Refund
							? z.TransactionData.Amount * -1
							: z.TransactionData.Amount)
					})
				.Project(x =>  new UserStatusView
					{
						SourceUserData = new Views.UserData { UserID = x.Key.PayerID },
						DestUserData = new Views.UserData { UserID = x.Key.PayeeID },
						UserBalance = x.TransactionSum
					});

			var payerViewPipeline = PipelineDefinition<TransactionHistory, UserStatusView>.Create(groupByPayerAggregator.Stages);

			// These users will not have a record in the previous pipeline so it is safe to combine them
			// Balance logic has to be inverted here
			//var usersWithoutPaymentsAggregator = transactionCollection
			//	.Aggregate()
			//	.Match(x => usersWithoutPaymentIDs.Contains(x.DestUserData.UserID))
			//	.Group(x => x.DestUserData.UserID,
			//		x => new
			//		{
			//			PayeeID = x.Key,
			//			TransactionsPerPayer = x
			//				.GroupBy(y => y.SourceUserData.UserID)
			//				.Select(y => new
			//				{
			//					PayerID = y.Key,
			//					TransactionsSum = y
			//						.Sum(z => z.TransactionData.TransactionType == TransactionType.Refund
			//							? z.TransactionData.Amount
			//							: z.TransactionData.Amount * -1)
			//				})
			//		})	
			//	.Project(x => x.TransactionsPerPayer
			//		.Select(y => new UserStatusView
			//		{
			//			SourceUserData = new Views.UserData { UserID = x.PayeeID },
			//			DestUserData = new Views.UserData { UserID = y.PayerID },
			//			UserBalance = y.TransactionsSum
			//		}));

			// Combine the pipelines
			//var combinedAggreggator = usersWithoutPaymentsAggregator
			//	.UnionWith(transactionCollection, payerViewPipeline);

			//var customerViewPipeline = PipelineDefinition<TransactionHistory, IEnumerable<UserStatusView>>.Create(combinedAggreggator.Stages);

			try
			{
				db.CreateView(_userStateViewName, config.TransactionHistoryCollectionName, payerViewPipeline);
			}
			catch
			{

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
