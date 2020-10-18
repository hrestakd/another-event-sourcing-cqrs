using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSSplitWise.DTO.Read;
using CQRSSplitWise.Filters.Read;
using CQRSSplitWise.Services.Read;
using Microsoft.AspNetCore.Mvc;

namespace CQRSSplitWise.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserHistoryController : ControllerBase
	{
		private readonly UserHistoryService _userHistoryService;

		public UserHistoryController(UserHistoryService userHistoryService)
		{
			_userHistoryService = userHistoryService;
		}

		[HttpGet("[action]")]
		public async Task<IEnumerable<UserHistoryDTO>> GetUserHistory(UserHistoryFilter filter)
		{
			var results = await _userHistoryService.GetUserHistory(filter);

			return results;
		}
	}
}
