using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSSplitWise.DAL.Models;
using CQRSSplitWise.Models.Enums;

namespace CQRSSplitWise.Filters.Read
{
	public class UserHistoryFilter : TransactionFilterBase
	{
		public int UserID { get; set; }
		public string UserName { get; set; }
		public string UserLastName { get; set; }
	}
}
