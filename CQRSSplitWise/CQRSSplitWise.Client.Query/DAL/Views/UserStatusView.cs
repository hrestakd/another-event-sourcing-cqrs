namespace CQRSSplitWise.Client.Query.DAL.Views
{
	public class UserStatusView
	{
		public UserData SourceUserData { get; set; }
		public UserData DestUserData { get; set; }
		public decimal UserBalance { get; set; }
	}
}
