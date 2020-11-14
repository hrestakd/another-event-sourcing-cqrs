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
	public class GroupQueryController : ControllerBase
	{
		private readonly GroupQueryService _groupQueryervice;

		public GroupQueryController(GroupQueryService groupQueryervice)
		{
			_groupQueryervice = groupQueryervice;
		}

		[HttpGet("[action]")]
		public async Task<IEnumerable<GroupHistoryDTO>> GetGroupHistory(GroupHistoryFilter filter)
		{
			var results = await _groupQueryervice.GetGroupHistory(filter);

			return results;
		}

		[HttpGet("[action]")]
		public async Task GetGroupState(int groupID)
		{
			await _groupQueryervice.GetGroupState(groupID);
		}
	}
}
