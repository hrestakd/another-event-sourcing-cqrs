using System.Collections.Generic;

namespace CQRSSplitWise.Client.Command.Models.Dto
{
	public class GroupUsersDTO
	{
		public int GroupId { get; set; }
		public string GroupName { get; set; }
		public IEnumerable<UserDTO> Users { get; set; }
	}
}
