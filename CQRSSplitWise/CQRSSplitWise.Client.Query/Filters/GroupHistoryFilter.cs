namespace CQRSSplitWise.Client.Query.Filters
{
	public class GroupHistoryFilter : TransactionFilterBase
	{
		public int GroupID { get; set; }
		public string GroupName { get; set; }
	}
}
