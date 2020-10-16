using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSSplitWise.Config
{
	public class GroupStateDBSettings:NoSQLDBSettings
	{
		public string GroupStateCollectionName { get; set; }
	}
}
