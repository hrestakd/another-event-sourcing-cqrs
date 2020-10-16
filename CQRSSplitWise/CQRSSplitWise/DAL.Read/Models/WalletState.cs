using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSSplitWise.DAL.Read.Models
{
	public class WalletState : ReadModelBase
	{
		public int WalletID { get; set; }
		public string WalletName { get; set; }
		public decimal Amount { get; set; }
	}
}
