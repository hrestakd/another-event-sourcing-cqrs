using CQRSSplitWise.DataContracts.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace CQRSSplitWise.Client.Query.DAL.Models
{
	public class TransactionData
	{
		public DateTime TransactionDate { get; set; }
		[BsonRepresentation(BsonType.Decimal128)]
		public decimal Amount { get; set; }
		public string Description { get; set; }
	}
}
