using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CQRSSplitWise.Client.Query.DAL.Models
{
	public class UserData
	{
		[BsonId]
		public ObjectId ID { get; set; }
		public int UserID { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
	}
}
