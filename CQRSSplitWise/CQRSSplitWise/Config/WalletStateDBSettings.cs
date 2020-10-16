using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSSplitWise.Config
{
	public class WalletStateDBSettings:NoSQLDBSettings
	{
		public string WalletStateCollectionName { get; set; }
	}
}
