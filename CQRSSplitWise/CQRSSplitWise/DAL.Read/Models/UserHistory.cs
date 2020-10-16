using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSSplitWise.DAL.Read.Models
{
	public class UserHistory : ReadModelBase
	{
		public UserData UserData { get; set; }
		public IEnumerable<Transaction> Transactions { get; set; }
	}
}
