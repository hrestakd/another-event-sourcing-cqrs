using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSSplitWise.Client.Query.DAL.Models
{
	public class UserBalance
	{
		[BsonId]
		public ObjectId ID { get; set; }
		public UserData SourceUserData { get; set; }
		public UserData DestUserData { get; set; }
		[BsonRepresentation(BsonType.Decimal128)]
		public decimal TotalBalance { get; set; }
	}
}
