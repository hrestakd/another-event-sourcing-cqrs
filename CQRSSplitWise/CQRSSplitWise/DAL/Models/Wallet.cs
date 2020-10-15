using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CQRSSplitWise.DAL.Models
{
	public class Wallet
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int WalletId { get; set; }
		public int UserId { get; set; }
		public string Name { get; set; }

		[ForeignKey("UserId")]
		public virtual User User { get; set; }
	}
}
