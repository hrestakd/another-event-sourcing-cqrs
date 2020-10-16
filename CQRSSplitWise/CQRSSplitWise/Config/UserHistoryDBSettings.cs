using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSSplitWise.Config
{
	public class UserHistoryDBSettings:NoSQLDBSettings
	{
		public string UserHistoryCollectionName { get; set; }
	}
}
