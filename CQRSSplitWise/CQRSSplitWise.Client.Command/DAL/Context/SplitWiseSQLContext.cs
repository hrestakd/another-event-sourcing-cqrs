using CQRSSplitWise.Client.Command.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRSSplitWise.Client.Command.DAL.Context
{
	public class SplitWiseSQLContext : DbContext
	{
		public DbSet<Group> Groups { get; set; }
		public DbSet<Transaction> Transactions { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Wallet> Wallets { get; set; }
		public DbSet<GroupUser> GroupUsers { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<GroupUser>()
				.HasKey(gu => new { gu.GroupId, gu.UserId });
			modelBuilder.Entity<GroupUser>()
				.HasOne(gu => gu.User)
				.WithMany(x => x.GroupUsers)
				.HasForeignKey(gu => gu.UserId);
			modelBuilder.Entity<GroupUser>()
				.HasOne(gu => gu.Group)
				.WithMany(x => x.GroupUsers)
				.HasForeignKey(gu => gu.GroupId);

			base.OnModelCreating(modelBuilder);
		}
		public SplitWiseSQLContext(DbContextOptions<SplitWiseSQLContext> options) : base(options)
		{
			Database.EnsureCreated();
		}
	}
}
