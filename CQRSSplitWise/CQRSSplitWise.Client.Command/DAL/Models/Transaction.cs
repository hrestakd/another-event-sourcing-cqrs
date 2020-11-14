using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CQRSSplitWise.DataContracts.Enums;

namespace CQRSSplitWise.Client.Command.DAL.Models
{
	public class Transaction
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int TransactionId { get; set; }
		public int UserId { get; set; }
		public int? SourceWalletId { get; set; }
		public int DestinationWalletId { get; set; }
		public TransactionType TransactionType { get; set; }
		public DateTime DateCreated { get; set; }
		public string Description { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal Amount { get; set; }

		[ForeignKey("SourceWalletId")]
		public virtual Wallet SourceWallet { get; set; }
		[ForeignKey("DestinationWalletId")]
		public virtual Wallet DestinationWallet { get; set; }
	}
}
