using CQRSSplitWise.Config;
using CQRSSplitWise.DAL.Read.Models;
using CQRSSplitWise.DAL.Read.Views;
using CQRSSplitWise.Models.Enums;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSSplitWise.DAL.Read
{
	public class UserStatusViewRepository
	{
		private readonly string _userStateViewName = "UserState";
		public UserStatusViewRepository(TransactionHistoryDBSettings config)
		{
			var client = new MongoClient(config.ConnectionString);
			var db = client.GetDatabase(config.DatabaseName);

			var transactionCollection = db.GetCollection<TransactionHistory>(config.TransactionHistoryCollectionName);

			var groupByPayerAggregator = transactionCollection
				.Aggregate()
				.Group(x => x.SourceUserData.UserID,
					x => new
					{
						PayerID = x.Key,
						TransactionsPerPayee = x
							.GroupBy(y => y.DestUserData.UserID)
							.Select(y => new
							{
								PayeeID = y.Key,
								TransactionsSum = y
									.Sum(z => z.TransactionData.TransactionType == TransactionType.Refund
										? z.TransactionData.Amount * -1
										: z.TransactionData.Amount)
							})
					})
				.Project(x => x.TransactionsPerPayee
					.Select(y => new UserStatusView
					{
						SourceUserData = new Views.UserData { UserID = x.PayerID },
						DestUserData = new Views.UserData { UserID = y.PayeeID },
						UserBalance = y.TransactionsSum
					}));

			var customerViewPipeline = PipelineDefinition<TransactionHistory, UserStatusView>.Create(groupByPayerAggregator.Stages);

			db.CreateView(_userStateViewName, config.TransactionHistoryCollectionName, customerViewPipeline);

			// What about users that never paid anything...............
			//var groupByPayee = transactionCollection
			//	.Aggregate()
			//	.Group(x => x.TransactionData.DestUserID,
			//		x => new
			//		{
			//			PayeeID = x.Key,
			//			TransactionsPerPayee = x
			//				.GroupBy(y => y.)
			//				.Select(y => new
			//				{
			//					PayeeID = y.Key,
			//					TransactionsSum = y
			//						.Sum(z => z.TransactionData.TransactionType == TransactionType.Refund
			//							? z.TransactionData.Amount * -1
			//							: z.TransactionData.Amount)
			//				})
			//		});
		}
	}
}
