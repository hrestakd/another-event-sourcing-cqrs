using CQRSSplitWise.DataContracts.Enums;
using System;

namespace CQRSSplitWise.Client.Query.Filters
{
	public abstract class TransactionFilterBase
	{
		public DateTime? CreatedFrom { get; set; }
		public DateTime? CreatedTo { get; set; }
		public decimal? AmountFrom { get; set; }
		public decimal? AmountTo { get; set; }
		public TransactionType TransactionType { get; set; }
	}
}
