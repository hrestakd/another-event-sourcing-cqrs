using System;
using CQRSSplitWise.DataContracts.Enums;

namespace CQRSSplitWise.DataContracts.Events
{
	public class TransactionEventData
	{
		public int SourceUserId { get; set; }
		public string SourceUserFirstName { get; set; }
		public string SourceUserLastName { get; set; }
		public int DestUserId { get; set; }
		public string DestUserFirstName { get; set; }
		public string DestUserLastName { get; set; }
		public TransactionType TransactionType { get; set; }
		public DateTime DateCreated { get; set; }
		public string Description { get; set; }
		public decimal Amount { get; set; }
	}
}
