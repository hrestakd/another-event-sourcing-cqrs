using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace CQRSSplitWise.DAL.Read.Models
{
	public class TransactionHistory
	{
		[BsonId]
		public ObjectId ID { get; set; }
		public Guid Guid { get; set; }
		public GroupData GroupData { get; set; }
		public UserData SourceUserData { get; set; }
		public UserData DestUserData { get; set; }
		public TransactionData TransactionData { get; set; }
	}
}
