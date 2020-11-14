using CQRSSplitWise.Models.Enums;

namespace CQRSSplitWise.Models.Dto
{
	public class Transaction
	{
		public int UserId { get; set; }
		public int? SourceWalletId { get; set; }
		public int DestinationWalletId { get; set; }
		public TransactionType TransactionType { get; set; }
		public string Description { get; set; }
		public decimal Amount { get; set; }
	}
}
