using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSSplitWise.DAL.Models;
using CQRSSplitWise.Models.Enums;

namespace CQRSSplitWise.Filters.Read
{
	public class UserHistoryFilter
	{
		public int UserID { get; set; }
		public string UserName { get; set; }
		public string UserLastName { get; set; }
		public DateTime? CreatedFrom { get; set; }
		public DateTime? CreatedTo { get; set; }
		public decimal? AmountFrom { get; set; }
		public decimal? AmountTo { get; set; }
		public TransactionType TransactionType { get; set; }

		internal bool DateRangeFilterSet { get { return CreatedFrom.HasValue || CreatedTo.HasValue; } }
		internal bool AmountRangeFilterSet { get { return AmountFrom.HasValue || AmountTo.HasValue; } }
	}
}
