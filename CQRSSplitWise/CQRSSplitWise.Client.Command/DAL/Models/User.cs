using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CQRSSplitWise.Client.Command.DAL.Models
{
	public class User
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public virtual Wallet Wallet { get; set; }
		public virtual ICollection<GroupUser> GroupUsers { get; set; }
	}
}
