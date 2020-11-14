namespace CQRSSplitWise.DAL.Models
{
	public class GroupUser
	{
		public int GroupId { get; set; }
		public int UserId { get; set; }

		public virtual Group Group { get; set; }
		public virtual User User { get; set; }
	}
}
