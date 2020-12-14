using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace CQRSSplitWise.Client.Query.DAL.Models
{
	public class GroupData
	{
		[BsonId]
		public ObjectId ID { get; set; }
		public int GroupID { get; set; }
		public string GroupName { get; set; }
		public IEnumerable<UserData> UsersInGroup { get; set; }
	}
}
