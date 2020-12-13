using System.Collections.Generic;

namespace CQRSSplitWise.Client.Query.UsersBalance.DTO
{
	public class UserBalanceDTO
	{
		public UserDTO UserData { get; set; }
		public decimal TotalBalance { get; set; }
		public IEnumerable<BalanceForUser> Balances { get; set; }
	}
}
