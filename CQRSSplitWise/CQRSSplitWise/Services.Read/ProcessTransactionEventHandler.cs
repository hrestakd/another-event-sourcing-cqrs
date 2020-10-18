using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSSplitWise.DAL.Read;
using CQRSSplitWise.DAL.Read.Models;

namespace CQRSSplitWise.Services.Read
{
	public class ProcessTransactionEventHandler
	{
		private readonly IQueryRepository<TransactionHistory> _repository;

		public ProcessTransactionEventHandler(IQueryRepository<TransactionHistory> repository)
		{
			_repository = repository;
		}

		public async Task ProcessEvent()
		{
			//var response = await _repository.InsertData(transaction);
			//return response;

			return;
		}
	}
}
