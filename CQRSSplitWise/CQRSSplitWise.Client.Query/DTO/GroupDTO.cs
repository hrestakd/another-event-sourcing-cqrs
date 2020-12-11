using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSSplitWise.Client.Query.DTO
{
	public class GroupDTO
	{
		public int GroupID { get; set; }
		public string GroupName { get; set; }
		public IEnumerable<UserDTO> UsersInGroup { get; set; }
	}
}
