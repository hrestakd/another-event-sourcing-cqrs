using System;

namespace CQRSSplitWise.DataContracts.Events
{
	public record CreateTransactionEvent(int SourceUserID, int DestUserID, DateTime TransactionDate, string Description, decimal Amount);
}
