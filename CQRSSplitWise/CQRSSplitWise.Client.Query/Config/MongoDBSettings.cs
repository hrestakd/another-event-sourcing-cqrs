namespace CQRSSplitWise.Client.Query.Config
{
	public class MongoDBSettings : NoSQLDBSettings
	{
		public string TransactionsCollectionName { get; set; }
		public string UsersCollectionName { get; set; }
		public string GroupsCollectionName { get; set; }
	}
}
