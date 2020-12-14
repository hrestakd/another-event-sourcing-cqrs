using System.Collections.Generic;

namespace CQRSSplitWise.Client.Command.Models.BindingModel
{
	public class AddGroupUsers
	{
		public int GroupId { get; set; }
		public IEnumerable<int> UserIds { get; set; }
	}
}
