using System.Collections.Generic;

namespace CQRSSplitWise.Models.Dto
{
	public class GroupUsers
	{
		public int GroupId { get; set; }
		public string GroupName { get; set; }
		public IEnumerable<User> Users { get; set; }
	}
}
