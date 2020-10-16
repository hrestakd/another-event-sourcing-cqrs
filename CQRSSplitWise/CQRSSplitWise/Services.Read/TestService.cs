using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSSplitWise.DAL.Read;
using CQRSSplitWise.DAL.Read.Models;

namespace CQRSSplitWise.Services.Read
{
	public class TestService
	{
		private readonly IQueryRepository<TransactionHistory> _repository;

		public TestService(IQueryRepository<TransactionHistory> repository)
		{
			_repository = repository;
		}

		public async Task<TransactionHistory> SaveDummyTransaction()
		{
			var transaction = new TransactionHistory
			{
				UserData = new UserData
				{
					UserID = 1,
					Name = "Name",
					LastName = "LastName"
				},
				GroupData = new GroupData
				{
					GroupID = 1,
					GroupName = "Group name"
				},
				TransactionData = new TransactionData
				{
					GroupID = 1,
					Description = "Description",
					Amount = 100m,
					DestWalletName = "Dest wallet",
					SourceWalletName = "Src wallet",
					TransactionDate = DateTime.UtcNow,
					TransactionType = Models.Enums.TransactionType.Payment
				}
			};

			var response = await _repository.InsertData(transaction);
			return response;
		}
	}
}
