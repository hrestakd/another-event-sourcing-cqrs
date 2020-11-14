using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSSplitWise.DAL.Read.Views;
using CQRSSplitWise.DTO.Read;
using CQRSSplitWise.Filters.Read;
using CQRSSplitWise.Services.Read;
using Microsoft.AspNetCore.Mvc;

namespace CQRSSplitWise.Controllers
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
