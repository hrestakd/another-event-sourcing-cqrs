using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSSplitWise.Config
{
	public class GroupHistoryDBSettings: NoSQLDBSettings
	{
		public string GroupHistoryCollectionName { get; set; }
	}
}
