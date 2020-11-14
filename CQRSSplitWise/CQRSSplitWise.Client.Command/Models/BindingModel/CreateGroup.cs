using System.Collections.Generic;

namespace CQRSSplitWise.Models.BindingModel
{
	public class CreateGroup
	{
		public string Name { get; set; }
		public IEnumerable<int> GroupUserIds { get; set; }
	}
}
