namespace CQRSSplitWise.Client.Query.Filters
{
	public class UserHistoryFilter : TransactionFilterBase
	{
		public int UserID { get; set; }
		public string UserName { get; set; }
		public string UserLastName { get; set; }
	}
}
