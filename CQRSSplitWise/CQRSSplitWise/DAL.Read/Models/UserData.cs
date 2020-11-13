using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSSplitWise.DAL.Read.Models
{
	public class UserData
	{
		public int UserID { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public IEnumerable<int> GroupIDs { get; set; } = Enumerable.Empty<int>();
	}
}
