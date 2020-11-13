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
	public class GroupHistoryController:ControllerBase
	{
		private readonly GroupHistoryService _groupHistoryService;

		public GroupHistoryController(GroupHistoryService groupHistoryService)
		{
			_groupHistoryService = groupHistoryService;
		}

		[HttpGet("[action]")]
		public async Task<IEnumerable<GroupHistoryDTO>> GetGroupHistory(GroupHistoryFilter filter)
		{
			var results = await _groupHistoryService.GetGroupHistory(filter);

			return results;
		}
	}
}
