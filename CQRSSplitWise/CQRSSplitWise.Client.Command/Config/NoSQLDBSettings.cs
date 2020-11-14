using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSSplitWise.Config
{
	public abstract class NoSQLDBSettings
	{
		public string ConnectionString { get; set; }
		public string DatabaseName { get; set; }
	}
}
