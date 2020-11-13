using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSSplitWise.Models.Enums;

namespace CQRSSplitWise.Filters.Read
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
