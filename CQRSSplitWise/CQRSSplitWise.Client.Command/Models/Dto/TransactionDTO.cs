using CQRSSplitWise.DataContracts.Enums;
using System;

namespace CQRSSplitWise.Client.Command.Models.Dto
{
	public class TransactionDTO
	{
		public int SourceUserId { get; set; }
		public int DestUserID { get; set; }
		public string Description { get; set; }
		public decimal Amount { get; set; }
		public DateTime TransactionDate { get; set; }
	}
}
