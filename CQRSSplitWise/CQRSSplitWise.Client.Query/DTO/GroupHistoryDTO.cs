using CQRSSplitWise.DataContracts.Enums;
using System;

namespace CQRSSplitWise.Client.Query.DTO
{
	public class GroupHistoryDTO
	{
		public string Name { get; set; }
		public DateTime TransactionDate { get; set; }
		public string SourceWalletName { get; set; }
		public string DestWalletName { get; set; }
		public decimal Amount { get; set; }
		public string Description { get; set; }
		public TransactionType TransactionType { get; set; }
	}
}
