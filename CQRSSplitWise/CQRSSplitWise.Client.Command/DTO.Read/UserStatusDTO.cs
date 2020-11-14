using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSSplitWise.DTO.Read
{
	public class UserStatusDTO
	{
		public string Name { get; set; }
		public string LastName { get; set; }
		public IEnumerable<BalanceForUser> Balances { get; set; }
	}
}
