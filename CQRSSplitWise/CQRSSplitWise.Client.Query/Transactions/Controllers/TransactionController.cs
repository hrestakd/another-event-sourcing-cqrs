using CQRSSplitWise.Client.Query.Transactions.DTO;
using CQRSSplitWise.Client.Query.Transactions.Filters;
using CQRSSplitWise.Client.Query.Transactions.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CQRSSplitWise.Client.Query.Transactions.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TransactionController : ControllerBase
	{
		private readonly TransactionService _transactionService;

		public TransactionController(TransactionService transactionService)
		{
			_transactionService = transactionService;
		}

		[HttpGet("[action]")]
		public async Task<IEnumerable<TransactionDTO>> GetTransactions(int allForUserID, int paidByID, int paidToID)
		{
			var filter = new TransactionFilter
			{
				AllForUserID = allForUserID,
				PaidByUserID = paidByID,
				PaidToUserID = paidToID
			};

			var results = await _transactionService.GetTransactions(filter);

			return results;
		}

		[HttpGet("[action]")]
		public async Task RebuildTransactions()
		{
			await _transactionService.RebuildTransactions();

			return;
		}
	}
}
