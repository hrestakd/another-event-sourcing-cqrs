using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSSplitWise.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CQRSSplitWise.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TransactionController : ControllerBase
	{
		private readonly ILogger<TransactionController> _logger;

		public TransactionController(ILogger<TransactionController> logger)
		{
			_logger = logger;
		}

		[HttpPost]
		public async Task<Transaction> Add()
		{
			return new Transaction();
		}
	}
}
