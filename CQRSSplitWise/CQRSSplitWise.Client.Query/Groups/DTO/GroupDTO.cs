using System.Collections.Generic;

namespace CQRSSplitWise.Client.Query.Groups.DTO
{
	public class GroupDTO
	{
		public int GroupID { get; set; }
		public string GroupName { get; set; }
		public IEnumerable<UserDTO> UsersInGroup { get; set; }
	}
}
