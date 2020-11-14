using System.Collections.Generic;

namespace CQRSSplitWise.Client.Command.Models.BindingModel
{
	public class CreateGroup
	{
		public string Name { get; set; }
		public IEnumerable<int> GroupUserIds { get; set; }
	}
}
