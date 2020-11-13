using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSSplitWise.DAL.Read.Views
{
	public class UserStatusView
	{
		public UserData SourceUserData { get; set; }
		public UserData DestUserData { get; set; }
		public decimal UserBalance { get; set; }
	}

	public class UserData
	{
		public int UserID { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
	}
}
