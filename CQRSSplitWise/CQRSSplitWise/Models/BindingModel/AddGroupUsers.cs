using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSSplitWise.Models.BindingModel
{
	public class AddGroupUsers
	{
		public int GroupId { get; set; }
		public IEnumerable<int> UserIds { get; set; }
	}
}
