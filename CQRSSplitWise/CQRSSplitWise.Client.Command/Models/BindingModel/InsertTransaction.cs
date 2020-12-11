using CQRSSplitWise.DataContracts.Enums;

namespace CQRSSplitWise.Client.Command.Models.BindingModel
{
	public class InsertTransaction
	{
		public int SourceUserId { get; set; }
		public int DestUserID { get; set; }
		public string Description { get; set; }
		public decimal Amount { get; set; }
	}
}
