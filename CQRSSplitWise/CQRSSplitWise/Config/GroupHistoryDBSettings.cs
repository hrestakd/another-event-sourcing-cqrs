using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSSplitWise.Config
{
	public class TransactionHistoryDBSettings: NoSQLDBSettings
	{
		public string TransactionHistoryCollectionName { get; set; }
	}
}
