using System;

namespace CQRSSplitWise.Client.Query.Transactions.DTO
{
	public class TransactionDTO
	{
		public UserDTO SourceUser { get; set; }
		public UserDTO TargetUser { get; set; }
		public DateTime TransactionDate { get; set; }
		public string Description { get; set; }
		public decimal Amount { get; set; }
	}
}
