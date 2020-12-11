using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSSplitWise.Client.Query.Filters
{
	public class GroupFilter
	{
		public int GroupID { get; set; }
		public string GroupName { get; set; }
		public int GroupsWithUserID { get; set; }
	}
}
