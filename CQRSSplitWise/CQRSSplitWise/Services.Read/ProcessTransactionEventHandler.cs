using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRSSplitWise.DAL.Read;
using CQRSSplitWise.DAL.Read.Models;
using CQRSSplitWise.Domain.Events;
using Newtonsoft.Json;

namespace CQRSSplitWise.Services.Read
{
	public class ProcessTransactionEventHandler
	{
		private readonly IQueryRepository<TransactionHistory> _repository;

		public ProcessTransactionEventHandler(IQueryRepository<TransactionHistory> repository)
		{
			_repository = repository;
		}

		public async Task ProcessEvent(byte[] eventData)
		{
			var eventDataObj = Extensions.ByteArrayToObject(eventData);
			var typedEventData = eventDataObj as TransactionEventData;

			if (typedEventData == null)
			{
				return;
			}

			var transactionEvent = new TransactionEvent(typedEventData, null);
			//TODO: insert into mongodb eventstore collection
			//var response = await _repository.InsertData(transaction);
			//return response;
			return;
		}
	}
}
