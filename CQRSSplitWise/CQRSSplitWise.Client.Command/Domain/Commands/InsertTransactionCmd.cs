using CQRSSplitWise.Client.Command.Models.Dto;
using CQRSSplitWise.DataContracts.Enums;
using MediatR;

namespace CQRSSplitWise.Client.Command.Domain.Commands
{
	public class InsertTransactionCmd : IRequest<TransactionDTO>
	{
		public int UserId { get; set; }
		public int? SourceWalletId { get; set; }
		public int DestinationWalletId { get; set; }
		public TransactionType TransactionType { get; set; }
		public string Description { get; set; }
		public decimal Amount { get; set; }
	}
}
