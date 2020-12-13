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
	public class RebuildDatabaseController : ControllerBase
	{
		private readonly GroupService _groupService;
		private readonly TransactionService _transactionService;
		private readonly UserBalanceService _userBalanceService;
		private readonly UserService _userService;

		public RebuildDatabaseController(
			GroupService groupService,
			UserService userService,
			TransactionService transactionService,
			UserBalanceService userBalanceService)
		{
			_groupService = groupService;
			_userBalanceService = userBalanceService;
			_userService = userService;
			_transactionService = transactionService;
		}

		[HttpGet("[action]")]
		public async Task Rebuild()
		{
			await _userService.RebuildUsers();
			await _groupService.RebuildGroups();
			await _transactionService.RebuildTransactions();
			await _userBalanceService.RebuildBalance();
		}
	}
}
