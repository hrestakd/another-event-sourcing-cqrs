using System.Threading.Tasks;
using CQRSSplitWise.Client.Query.DTO;
using CQRSSplitWise.Client.Query.Services;
using Microsoft.AspNetCore.Mvc;

namespace CQRSSplitWise.Client.Query.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserSummaryController : ControllerBase
	{
		private readonly UserBalanceService _userBalanceService;

		public UserSummaryController(UserBalanceService userBalanceService)
		{
			_userBalanceService = userBalanceService;
		}

		[HttpGet("[action]")]
		public async Task<UserBalanceDTO> GetSummaryForUser(int userID)
		{
			var userSummary = await _userBalanceService.GetUserBalance(userID);

			return userSummary;
		}

		[HttpGet("[action]")]
		public async Task RebuildBalance()
		{
			 await _userBalanceService.RebuildBalance();

			return;
		}
	}
}
