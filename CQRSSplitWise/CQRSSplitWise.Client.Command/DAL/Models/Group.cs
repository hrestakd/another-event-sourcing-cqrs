using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CQRSSplitWise.Client.Command.DAL.Models
{
	public class Group
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int GroupId { get; set; }
		public string Name { get; set; }

		public virtual ICollection<GroupUser> GroupUsers { get; set; }
	}
}
