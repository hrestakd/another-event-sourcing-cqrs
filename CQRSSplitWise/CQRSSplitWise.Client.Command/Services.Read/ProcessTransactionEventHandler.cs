using System;
using System.Threading.Tasks;
using CQRSSplitWise.DAL.Read;
using CQRSSplitWise.DAL.Read.Models;
using CQRSSplitWise.Domain.Events;

namespace CQRSSplitWise.Services.Read
{
	public class ProcessTransactionEventHandler
	{
		private readonly IInsertRepository<TransactionHistory> _repository;

		public ProcessTransactionEventHandler(IInsertRepository<TransactionHistory> repository)
		{
			_repository = repository;
		}

		public async Task ProcessEvent(byte[] eventObj)
		{
			if (!(Extensions.ByteArrayToObject(eventObj) is TransactionEvent transactionEvent))
			{
				return;
			}

			var transactionEventData = transactionEvent.GetEventData<TransactionEventData>();
			var transaction = MapEventDataToTransactionHistory(transactionEventData, transactionEvent.EventID);
			await _repository.InsertData(transaction);
		}

		private TransactionHistory MapEventDataToTransactionHistory(TransactionEventData eventData, Guid eventID)
		{
			var transactionHistory = new TransactionHistory
			{
				Guid = eventID,
				GroupData = new GroupData(), // TODO: later
				SourceUserData = new UserData
				{
					UserID = eventData.SourceUserId,
					Name = eventData.SourceUserFirstName,
					LastName = eventData.SourceUserLastName
				},
				DestUserData = new UserData
				{
					UserID = eventData.DestUserId,
					Name = eventData.DestUserFirstName,
					LastName = eventData.DestUserLastName
				},
				TransactionData = new TransactionData
				{
					TransactionType = eventData.TransactionType,
					TransactionDate = eventData.DateCreated,
					Description = eventData.Description,
					Amount = eventData.Amount
				}
			};

			return transactionHistory;
		}
	}
}
