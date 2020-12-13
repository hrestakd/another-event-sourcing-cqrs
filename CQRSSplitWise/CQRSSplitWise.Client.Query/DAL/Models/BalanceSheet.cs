using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSSplitWise.Client.Query.DAL.Models
{
	public class BalanceSheet
	{
		public UserData UserData { get; set; }
		[BsonRepresentation(BsonType.Decimal128)]
		public decimal BalanceForUser { get; set; }
	}
}
