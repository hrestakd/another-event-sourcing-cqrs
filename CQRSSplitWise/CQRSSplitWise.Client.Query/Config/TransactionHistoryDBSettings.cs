namespace CQRSSplitWise.Client.Query.Config
{
	public class TransactionHistoryDBSettings : NoSQLDBSettings
	{
		public string TransactionHistoryCollectionName { get; set; }
		public string UsersCollectionName { get; set; }
		public string GroupsCollectionName { get; set; }
	}
}
