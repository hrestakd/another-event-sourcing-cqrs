using CQRSSplitWise.Client.Command.Models.Dto;
using CQRSSplitWise.DataContracts.Enums;
using MediatR;

namespace CQRSSplitWise.Client.Command.Domain.Commands
{
	public class InsertTransactionCmd : IRequest<TransactionDTO>
	{
		public int SourceUserId { get; set; }
		public int DestUserID { get; set; }
		public string Description { get; set; }
		public decimal Amount { get; set; }
	}
}
