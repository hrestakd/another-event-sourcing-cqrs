using System;
using CQRSSplitWise.Models.Enums;

namespace CQRSSplitWise.DAL.Read.Models
{
	public class Transaction
	{
		public int GroupID { get; set; }
		public DateTime TransactionDate { get; set; }
		public string SourceWalletName { get; set; } 
		public string DestWalletName { get; set; }
		public decimal Amount { get; set; }
		public string Description { get; set; }
		public TransactionType TransactionType { get; set; }
	}
}
