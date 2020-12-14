using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace CQRSSplitWise.Client.Query.DAL.Models
{
	public class Transaction
	{
		[BsonId]
		public ObjectId ID { get; set; }
		public UserData SourceUserData { get; set; }
		public UserData DestUserData { get; set; }
		public TransactionData TransactionData { get; set; }
	}
}
