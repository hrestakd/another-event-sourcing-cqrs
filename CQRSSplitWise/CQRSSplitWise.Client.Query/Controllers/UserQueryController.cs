using System.Collections.Generic;
using System.Threading.Tasks;
using CQRSSplitWise.Client.Query.DTO;
using CQRSSplitWise.Client.Query.Filters;
using CQRSSplitWise.Client.Query.Services;
using Microsoft.AspNetCore.Mvc;

namespace CQRSSplitWise.Client.Query.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserQueryController : ControllerBase
	{
		private readonly UserQueryService _userQueryService;

		public UserQueryController(UserQueryService userQueryService)
		{
			_userQueryService = userQueryService;
		}

		[HttpGet("[action]")]
		public async Task<IEnumerable<UserHistoryDTO>> GetUserHistory()
		{
			var results = await _userQueryService.GetUserHistory(new UserHistoryFilter());

			return results;
		}

		[HttpGet("[action]")]
		public async Task<UserStatusDTO> GetUserState(int userID)
		{
			var userStatus = await _userQueryService.GetUserState(userID);

			return userStatus;
		}
	}
}
