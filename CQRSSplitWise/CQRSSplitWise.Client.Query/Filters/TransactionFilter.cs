using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSSplitWise.Client.Query.Filters
{
	public class TransactionFilter
	{
		public DateTime? CreatedFrom { get; set; }
		public DateTime? CreatedTo { get; set; }
		public decimal? AmountFrom { get; set; }
		public decimal? AmountTo { get; set; }
		public int AllForUserID { get; set; }
		public int PaidByUserID { get; set; }
		public int PaidToUserID { get; set; }
	}
}
