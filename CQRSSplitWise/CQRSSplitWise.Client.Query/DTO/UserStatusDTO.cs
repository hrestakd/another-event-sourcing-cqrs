using System.Collections.Generic;

namespace CQRSSplitWise.Client.Query.DTO
{
	public class UserStatusDTO
	{
		public string Name { get; set; }
		public string LastName { get; set; }
		public IEnumerable<BalanceForUser> Balances { get; set; }
	}
}
