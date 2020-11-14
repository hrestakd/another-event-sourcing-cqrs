using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSSplitWise.DAL.Models;
using CQRSSplitWise.Models.Enums;

namespace CQRSSplitWise.Filters.Read
{
	public class GroupHistoryFilter : TransactionFilterBase
	{
		public int GroupID { get; set; }
		public string GroupName { get; set; }
	}
}
