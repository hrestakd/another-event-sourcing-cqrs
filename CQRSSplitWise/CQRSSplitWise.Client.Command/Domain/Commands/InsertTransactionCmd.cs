using CQRSSplitWise.Models.Dto;
using CQRSSplitWise.Models.Enums;
using MediatR;

namespace CQRSSplitWise.Domain.Commands
{
	public class InsertTransactionCmd : IRequest<Transaction>
	{
		public int UserId { get; set; }
		public int? SourceWalletId { get; set; }
		public int DestinationWalletId { get; set; }
		public TransactionType TransactionType { get; set; }
		public string Description { get; set; }
		public decimal Amount { get; set; }
	}
}
