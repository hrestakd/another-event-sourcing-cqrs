using CQRSSplitWise.Client.Query.DTO;
using CQRSSplitWise.Client.Query.Filters;
using CQRSSplitWise.Client.Query.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSSplitWise.Client.Query.Controllers
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
		public async Task<IEnumerable<TransactionDTO>> GetTransactions()
		{
			var results = await _transactionService.GetTransactions(new TransactionFilter());

			return results;
		}
	}
}
