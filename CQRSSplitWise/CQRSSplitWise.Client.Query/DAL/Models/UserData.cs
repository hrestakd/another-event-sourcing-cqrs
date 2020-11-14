using System.Collections.Generic;
using System.Linq;

namespace CQRSSplitWise.Client.Query.DAL.Models
{
	public class UserData
	{
		public int UserID { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public IEnumerable<int> GroupIDs { get; set; } = Enumerable.Empty<int>();
	}
}
