using CQRSSplitWise.DataContracts.Enums;

namespace CQRSSplitWise.Client.Command.Models.Dto
{
	public class TransactionDTO
	{
		public int UserId { get; set; }
		public int? SourceWalletId { get; set; }
		public int DestinationWalletId { get; set; }
		public TransactionType TransactionType { get; set; }
		public string Description { get; set; }
		public decimal Amount { get; set; }
	}
}
